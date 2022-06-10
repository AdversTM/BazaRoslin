using System;
using System.Windows.Input;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class OfferDialogViewModel : DialogViewModelBase {

        private readonly IRegionManager _regionManager;
        private readonly IUserStore _userStore;

        private ICommand? _buyCommand;
        private ICommand? _rateCommand;
        private ICommand? _followCommand;

        private IUser _user = null!;
        private IOffer _offer = null!;
        private IPlant _plant = null!;
        private bool _following = true;
        private int _rating = 3;
        private bool _isBuyable;

        public ICommand BuyCommand => _buyCommand ??= new DelegateCommand(Buy, () => Offer.Availability > 0 && IsBuyable);
        public ICommand RateCommand => _rateCommand ??= new DelegateCommand<string>(Rate);
        public ICommand FollowCommand => _followCommand ??= new DelegateCommand(Follow);

        public IOffer Offer {
            get => _offer;
            set => SetProperty(ref _offer, value);
        }

        public IPlant Plant {
            get => _plant;
            set => SetProperty(ref _plant, value);
        }

        public bool Following {
            get => _following;
            set => SetProperty(ref _following, value);
        }

        public int Rating {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public bool IsBuyable {
            get => _isBuyable;
            set => SetProperty(ref _isBuyable, value);
        }

        public OfferDialogViewModel(IRegionManager regionManager, IUserStore userStore) {
            _regionManager = regionManager;
            _userStore = userStore;
        }

        public override void OnDialogOpened(IDialogParameters parameters) {
            _user = parameters.GetValue<IUser>("user");
            Offer = parameters.GetValue<IOffer>("offer");
            Plant = parameters.GetValue<IPlant>("plant");
            _isBuyable = parameters.GetValue<bool>("buyable");
            //TODO: rating
        }

        private void Buy() {
            
            IsBuyable = false;
            
            //TODO
        }

        private void Rate(string tag) {
            var i = int.Parse(tag);
            Rating = i + 1;
            //TODO
        }

        private void Follow() {
            Following = !Following;
            //TODO
        }
    }
}