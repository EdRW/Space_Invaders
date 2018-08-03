using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        private static InputManager pInstance = null;
        private bool privSpaceKeyPrev;
        private bool priv1KeyPrev;
        private bool priv2KeyPrev;
        private bool privBKeyPrev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectOne;
        private InputSubject pSubjectTwo;
        private InputSubject pSubjectB;

        private InputManager()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();
            this.pSubjectOne = new InputSubject();
            this.pSubjectTwo = new InputSubject();
            this.pSubjectB = new InputSubject();

            this.privSpaceKeyPrev = false;
            this.priv1KeyPrev = false;
            this.priv2KeyPrev = false;
            this.privBKeyPrev = false;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }

        public static InputSubject GetOneSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectOne;
        }

        public static InputSubject GetTwoSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectTwo;
        }

        public static InputSubject GetBSubject()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectB;
        }

        public static void Update()
        {
            InputManager pMan = InputManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                //Debug.WriteLine("Left Key Pressed");
                pMan.pSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                //Debug.WriteLine("Right Key Pressed");
                pMan.pSubjectArrowRight.Notify();
            }

            // SpaceKey: (with key history) -----------------------------------------------------------
            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                //Debug.WriteLine("Space Key Pressed");
                pMan.pSubjectSpace.Notify();
            }
            pMan.privSpaceKeyPrev = spaceKeyCurr;

            // 1Key: (with key history) -----------------------------------------------------------
            bool oneKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1);
            if (oneKeyCurr == true && pMan.priv1KeyPrev == false)
            {
                //Debug.WriteLine("One Key Pressed");
                pMan.pSubjectOne.Notify();
            }
            pMan.priv1KeyPrev = oneKeyCurr;

            // 2Key: (with key history) -----------------------------------------------------------
            bool twoKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2);
            if (twoKeyCurr == true && pMan.priv2KeyPrev == false)
            {
                //Debug.WriteLine("Two Key Pressed");
                pMan.pSubjectTwo.Notify();
            }
            pMan.priv2KeyPrev = twoKeyCurr;

            // BKey: (with key history) -----------------------------------------------------------
            bool BKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_B);
            if (BKeyCurr == true && pMan.privBKeyPrev == false)
            {
                //Debug.WriteLine("B Key Pressed");
                pMan.pSubjectB.Notify();
            }
            pMan.privBKeyPrev = BKeyCurr;

        }

        private static InputManager PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputManager();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }
    }
}
