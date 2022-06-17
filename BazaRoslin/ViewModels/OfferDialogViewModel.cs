using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class OfferDialogViewModel : DialogViewModelBase {

        private readonly IEventAggregator _eventAggregator;
        private readonly IPlantStore _plantStore;

        private DelegateCommand? _buyCommand;
        private ICommand? _rateCommand;
        private ICommand? _followCommand;

        private IUser _user = null!;
        private IOffer _offer = null!;
        private IPlant _plant = null!;
        private IOfferRating _rating = null!;
        private bool _isFollowing;
        private bool _isBuyable;

        public DelegateCommand BuyCommand => _buyCommand ??= new DelegateCommand(Buy, () => Offer.Availability > 0 && IsBuyable);
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

        public bool IsFollowing {
            get => _isFollowing;
            set => SetProperty(ref _isFollowing, value);
        }

        public int Rating {
            get => (int)_rating.Rating;
            set {
                _rating.Rating = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBuyable {
            get => _isBuyable;
            set => SetProperty(ref _isBuyable, value);
        }

        public OfferDialogViewModel(IEventAggregator eventAggregator, IPlantStore plantStore) {
            _eventAggregator = eventAggregator;
            _plantStore = plantStore;
            
        }

        public override void OnDialogOpened(IDialogParameters parameters) {
            _user = parameters.GetValue<IUser>("user");
            Offer = parameters.GetValue<IOffer>("offer");
            Plant = parameters.GetValue<IPlant>("plant");
            _isBuyable = parameters.GetValue<bool>("buyable");
            _rating = parameters.GetValue<IOfferRating>("offerRating");
            _isFollowing = parameters.GetValue<bool>("offerFollow");
        }

        private void Buy() {
            IsBuyable = false;
            BuyCommand.RaiseCanExecuteChanged();
            _plantStore.AddUserPlant(_user.Id, _plant.Id);
            _eventAggregator.GetEvent<UserPlantAddEvent>().Publish(_plant);
        }

        private void Rate(string tag) {
            var i = int.Parse(tag);
            Rating = i + 1;
            _plantStore.SetRating(_rating);
        }

        private void Follow() {
            IsFollowing = !IsFollowing;
            _plantStore.SetFollow(_offer.Id, _user.Id, IsFollowing);
            
            if (IsFollowing)
                _eventAggregator.GetEvent<OfferFollowEvent>().Publish(_offer.Id);
            else
                _eventAggregator.GetEvent<OfferUnfollowEvent>().Publish(_offer.Id);
        }
    }
}