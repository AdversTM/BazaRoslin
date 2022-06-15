using System.Windows;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace BazaRoslin.ViewModels {
    public class UserDetailsViewModel : BindableBase, INavigationAware {

        private readonly IEventAggregator _eventAggregator;
        private readonly IPlantStore _plantStore;
        private readonly IAuthService _authService;
        
        private IPlant? _plant;
        
        private ICommand? _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(Delete);

        public IPlant? Plant {
            get => _plant;
            set => SetProperty(ref _plant, value);
        }

        public UserDetailsViewModel(IEventAggregator eventAggregator, IPlantStore plantStore, IAuthService authService) {
            _eventAggregator = eventAggregator;
            _plantStore = plantStore;
            _authService = authService;
            // eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(u => _user = u);
        }

        public void OnNavigatedTo(NavigationContext navigationContext) {
            Plant = navigationContext.Parameters.GetValue<IPlant>("Plant");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {
            Plant = null;
        }

        private void Delete() {
            if (MessageBox.Show("Czy na pewno usunąć posiadaną roślinę?", "Usuwanie rośliny",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;
            
            var id = _plant!.Id;
            _plantStore.DeletePlant(id, _authService.LoggedUser.Id);
            _eventAggregator.GetEvent<DeleteUserPlantEvent>().Publish(id);
        }
    }
}