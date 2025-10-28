using BepInEx;

namespace ZkMenu.src.Loading
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Loader : BaseUnityPlugin
    {
        public void Start()
        {
            Main.start();
        }

        public void Awake()
        {
            Main.awake();
        }

        public void Update()
        {
            Main.update();
        }

        public void OnEnabled()
        {
            Main.enabled();
        }

        public void OnDisabled()
        {
            Main.disabled();
        }
    }
}
