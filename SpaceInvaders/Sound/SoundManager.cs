using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SoundManager_Link : Manager
    {
        public Image_Link poActive = null;
        public Image_Link poReserve = null;

        protected SoundManager_Link(int numNodes = 9, int growthSize = 1)
            : base(numNodes, growthSize)
        {
        }
    }

    public class SoundManager : SoundManager_Link
    {
        private static SoundManager pInstance = null;
        private Sound poCompareNode;
        public IrrKlang.ISoundEngine poSndEngine;


        protected SoundManager(int numNodes = 9, int growthSize = 1)
            : base(numNodes, growthSize)
        {
            poSndEngine = new IrrKlang.ISoundEngine();
            poCompareNode = new Sound();
            Debug.Assert(poCompareNode != null);
        }

        public static void Create(int numNodes = 9, int growthSize = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SoundManager(numNodes, growthSize);
            }
        }

        public static Sound Add(Sound.Name soundName, float volume = 1)
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Sound pNode = (Sound)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(soundName, volume);

            return pNode;
        }

        public static void Remove(Sound pNode)
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Sound Find(Sound.Name name)
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            Sound pData = (Sound)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static IrrKlang.ISound PlaySound(Sound.Name name, bool loop = false)
        {
            Sound pSound = Find(name);
            Debug.Assert(pSound != null);

            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.poSndEngine != null);

            IrrKlang.ISound hardwareSound = pMan.poSndEngine.Play2D(pSound.GetSound(), loop, false, false);

            return hardwareSound;
        }

        public static void StopAllSounds()
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.poSndEngine != null);
            pMan.poSndEngine.StopAllSounds();
        }

        public static IrrKlang.ISoundEngine GetEngine()
        {
            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.poSndEngine != null);
            return pMan.poSndEngine;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("--------------------------------- Sound Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            SoundManager pMan = SoundManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static SoundManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Sound();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((Sound)node1).name == ((Sound)node2).name;
        }
    }
}
