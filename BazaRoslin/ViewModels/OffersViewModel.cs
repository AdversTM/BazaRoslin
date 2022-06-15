using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using BazaRoslin.Util;
using ImTools;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class OffersViewModel : BindableBase, INavigationAware {

        private readonly IPlantStore _plantStore;
        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;

        private ICommand? _offerCommand;
        private IPlant _plant = null!;
        private ObservableCollection<IOffer> _offers = new();
        private HashSet<int> _userPlants = new();

        public ObservableCollection<IOffer> Offers {
            get => _offers;
            set => SetProperty(ref _offers, value);
        }

        public ICommand OfferCommand => _offerCommand ??= new DelegateCommand<IOffer>(OpenOffer);

        public OffersViewModel(IPlantStore plantStore, IDialogService dialogService, IEventAggregator eventAggregator,
            IAuthService authService) {
            _plantStore = plantStore;
            _dialogService = dialogService;
            _authService = authService;
            // eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(u => _user = u);
            eventAggregator.GetEvent<DeleteUserPlantEvent>().Subscribe(id => _userPlants.Remove(id));
            eventAggregator.GetEvent<AddUserPlantEvent>().Subscribe(p => _userPlants.Add(p.Id));
        }

        public void OnNavigatedTo(NavigationContext navigationContext) {
            _plant = navigationContext.Parameters.GetValue<IPlant>("Plant");
            _plantStore.GetOffers(_plant.Id).ContinueWith(task =>
                Offers = new ObservableCollection<IOffer>(task.Result.Also(it => it.Sort())));
            _plantStore.GetPlants(_authService.LoggedUser.Id).ContinueWith(task =>
                _userPlants = task.Result.Map(p => p.Id).ToHashSet());
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {
        }

        private void OpenOffer(IOffer offer) {
            var buyable = !_userPlants.Contains(offer.PlantId);
            var param = new DialogParameters {
                { "user", _authService.LoggedUser }, { "offer", offer }, { "plant", _plant }, { "buyable", buyable }
            };
            _dialogService.Show("OfferDialog", param, _ => { });
        }
    }
}