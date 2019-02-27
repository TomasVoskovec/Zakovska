using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zakovska
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Zakovska : TabbedPage
    {
        public Zakovska ()
        {
            InitializeComponent();

            Children.Add(new Marks() { Title = "Známky"});
            Children.Add(new AddMark() { Title = "Přidat známky" });
            Children.Add(new AddSubject() { Title = "Přidat předmět" });
        }
    }
}