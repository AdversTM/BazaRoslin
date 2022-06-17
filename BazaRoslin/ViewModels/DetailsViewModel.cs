using BazaRoslin.Model;
using Prism.Mvvm;
using Prism.Regions;

namespace BazaRoslin.ViewModels {
    public class DetailsViewModel : BindableBase, INavigationAware {
        
        private IPlant? _plant;

        public IPlant? Plant {
            get => _plant;
            set => SetProperty(ref _plant, value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext) {
            Plant = navigationContext.Parameters.GetValue<IPlant>("plant");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {
            Plant = null;
        }
    }
}