using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InvaderGridManager
    {
        private static InvaderGridManager pInstance = null;

        private NotCollidingWithWallState pNotCollingState;
        private CollidingRightWallState pCollidingRight;
        private CollidingLeftWallState pCollidingLeft;

        // Used to randomly decide which columns to drop a bomb from
        private Random pRandom;

        public enum State
        {
            NotCollingWithWall,
            CollidingRightWall,
            CollidingLeftWall
        }

        public InvaderGridManager()
        {
            this.pNotCollingState = new NotCollidingWithWallState();
            this.pCollidingRight = new CollidingRightWallState();
            this.pCollidingLeft = new CollidingLeftWallState();
            this.pRandom = new Random();
        }

        public static GridState GetState(InvaderGridManager.State state)
        {
            InvaderGridManager pMan = InvaderGridManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GridState pGridState = null;

            switch (state)
            {
                case InvaderGridManager.State.NotCollingWithWall:
                    pGridState = pMan.pNotCollingState;
                    break;

                case InvaderGridManager.State.CollidingLeftWall:
                    pGridState = pMan.pCollidingLeft;
                    break;

                case InvaderGridManager.State.CollidingRightWall:
                    pGridState = pMan.pCollidingRight;
                    break;
            }

            return pGridState;
        }

        public static InvaderCategory GetRandomBombDropper(InvaderGrid pGrid)
        {
            InvaderGridManager pMan = InvaderGridManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            InvaderCategory pTmpInvader = null;

            // Find a random bottom row invader to drop the bomb
            int numColumns = pGrid.GetNumChildren();
            int randomColumnIndex = pMan.pRandom.Next(numColumns);
            InvaderColumn pColumn = (InvaderColumn)pGrid.GetChild(randomColumnIndex);
            pTmpInvader = (InvaderCategory)pColumn.GetChild(0);

            // Check to see if the invader drop the bomb, if not go find another invader
            
            InvaderCategory pInvader = null;
            for (int i = 0; i < numColumns; i++)
            {
                randomColumnIndex = pMan.pRandom.Next(numColumns);
                pColumn = (InvaderColumn)pGrid.GetChild(randomColumnIndex);
                pTmpInvader = (InvaderCategory)pColumn.GetChild(0);
                if (pTmpInvader.canLaunchBomb)
                {
                    pInvader = pTmpInvader;
                    break;
                }
            }

            return pInvader;
        }

        // TODO maybe this becomes a init crit method and make activategrid just display the grid rather than create a new one
        public static InvaderGrid GenerateGrid(int level)
        {
            // Get factory for producing gameobjects and adding them to the gameobject manager and spritebatches
            InvaderFactory rootLevelIF = new InvaderFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes);
            // create the grid composite data structure
            InvaderGrid pGrid = (InvaderGrid)rootLevelIF.ActiveCreate(GameObject.Type.InvaderGrid, 0, 0);

            // Get another factory for producing gameobjects and adding them to the Grid as well as the gameobject manager and sprite batches
            InvaderFactory columnIF = new InvaderFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pGrid);
            for (int i = 0; i < 11; i++)
            {
                InvaderColumn pColumn = (InvaderColumn)columnIF.ActiveCreate(GameObject.Type.InvaderColumn, 0, 0);

                InvaderFactory gridAliensIF = new InvaderFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes, pColumn);
                float xPos = Constants.gridXOrigin + i * Constants.gridColumnDelta;
                gridAliensIF.ActiveCreate(GameObject.Type.SmallInvader, xPos, Constants.smallInvaderYPos - level*10);
                gridAliensIF.ActiveCreate(GameObject.Type.MediumInvader, xPos, Constants.MediumInvaderYPos1 - level * 10);
                gridAliensIF.ActiveCreate(GameObject.Type.MediumInvader, xPos, Constants.MediumInvaderYPos2 - level * 10);
                gridAliensIF.ActiveCreate(GameObject.Type.LargeInvader, xPos, Constants.LargeInvaderYPos1 - level * 10);
                gridAliensIF.ActiveCreate(GameObject.Type.LargeInvader, xPos, Constants.LargeInvaderYPos2 - level * 10);
            }
            pGrid.SetState(InvaderGridManager.State.NotCollingWithWall);

            return pGrid;
        }

        public static void ActivateGrid(InvaderGrid pGrid, SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            Debug.Assert(pGrid != null);
            GameObjectManager.Attach(pGrid);

            SpriteBatch pSpriteBatch = SpriteBatchManager.Find(spriteBatchName);
            Debug.Assert(pSpriteBatch != null);

            SpriteBatch pBoxSpriteBatch = SpriteBatchManager.Find(boxSpriteBatchName);
            Debug.Assert(pBoxSpriteBatch != null);

            pBoxSpriteBatch.Attach(pGrid.poColObj.pColSprite);
            pSpriteBatch.Attach(pGrid.pProxySprite);

            ForwardIterator pFwdItor = new ForwardIterator(pGrid);

            GameObject pGameObj = (GameObject)pFwdItor.Next();
            while (!pFwdItor.IsDone())
            {
                GameObjectManager.Attach(pGameObj);

                pBoxSpriteBatch.Attach(pGameObj.poColObj.pColSprite);
                pSpriteBatch.Attach(pGameObj.pProxySprite);

                pGameObj = (GameObject)pFwdItor.Next();
            }
        }

        public static void DeactivateGrid(InvaderGrid pGrid)
        {
            Debug.Assert(pGrid != null);

            // Remove from SpriteBatch
            // Find the SBNode
            Debug.Assert(pGrid.pProxySprite != null);
            SpriteBaseNode pSBNode = pGrid.pProxySprite.GetSBNode();

            // Remove it from the manager
            Debug.Assert(pSBNode != null);
            SpriteBatchManager.Remove(pSBNode);

            // Remove collision sprite from spriteBatch
            Debug.Assert(pGrid.poColObj != null);
            Debug.Assert(pGrid.poColObj.pColSprite != null);
            pSBNode = pGrid.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            SpriteBatchManager.Remove(pSBNode);

            GameObjectManager.NonTreeRemove(pGrid);

            

            ForwardIterator pFwdItor = new ForwardIterator(pGrid);

            GameObject pGameObj = (GameObject)pFwdItor.Next();
            while (!pFwdItor.IsDone())
            {
                // Remove from SpriteBatch
                // Find the SBNode
                Debug.Assert(pGameObj.pProxySprite != null);
                pSBNode = pGameObj.pProxySprite.GetSBNode();

                // Remove it from the manager
                Debug.Assert(pSBNode != null);
                SpriteBatchManager.Remove(pSBNode);

                // Remove collision sprite from spriteBatch

                Debug.Assert(pGameObj.poColObj != null);
                Debug.Assert(pGameObj.poColObj.pColSprite != null);
                pSBNode = pGameObj.poColObj.pColSprite.GetSBNode();

                Debug.Assert(pSBNode != null);
                SpriteBatchManager.Remove(pSBNode);

                GameObjectManager.NonTreeRemove(pGameObj);

                pGameObj = (GameObject)pFwdItor.Next();
            }    
        }
        private static InvaderGridManager PrivGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InvaderGridManager();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

    }
}
