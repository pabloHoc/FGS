using System.IO;
using FSG.Core;
using FSG.Commands;
using Myra.Assets;
using Myra.Graphics2D.UI;

namespace FSG.UI
{
    public class EmpireInfoController : UIController
    {
        public EmpireInfoController(ServiceProvider serviceProvider, Desktop desktop, AssetManager assetManager)
            : base("../../../UI/EmpireInfo/EmpireInfo.xaml", serviceProvider, desktop, assetManager)
        {
        }

        public override void Update(ICommand command)
        {

        }
    }
}

