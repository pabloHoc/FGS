using System.IO;
using FSG.Core;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class EmpireDetailsController : UIController
    {
        public EmpireDetailsController(ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
            : base("../../../UI/EmpireDetails/EmpireDetails.xaml", serviceProvider, desktop, assetManager)
        {
        }

        public override void Update()
        {

        }
    }
}

