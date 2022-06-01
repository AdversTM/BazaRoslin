using System;
using BazaRoslin.ViewModels;

namespace BazaRoslin.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell {
        public Shell() {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            (DataContext as ShellViewModel)?.OnWindowActivated(this);
        }
    }
}