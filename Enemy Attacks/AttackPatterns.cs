using System;
using System.Collections.Generic;

namespace MyGame
{
    public class FirstBoneAttack : BaseWhiteAttackPattern //Attack for spawning left and right attacks at the same time.
    {
        public FirstBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 16 && listA.Count == 0 && listB.Count == 0)
            {
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 16) return;
            if (counter > 1.2f - (duration / 20)) //If 1 - (duration/20)" have passed since last attack was thrown:
            {
                AddAttack(listA, new Vector2(160, Engine.center.y + 90), Vector2.right * 5);
                AddAttack(listB, new Vector2(880, Engine.center.y - 90), Vector2.left * 5);
                counter = 0; //Reset attack timer.
                numOfAttacks += 2;
            }
        }

        public override void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            foreach (BaseBoneAttack attack in attackList)
            {
                Vector2 currentDir = attack.speed.normalized;
                attack.UpdateSpeed(currentDir * (3 + duration));
                attack.Update();
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count -1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.x > 880)
                {
                    factory.ReturnAttack(listA[i]);
                    listA.RemoveAt(i);
                }
            }

            for (int i = listB.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listB[i].transform.position.x < 160)
                {
                    factory.ReturnAttack(listB[i]);
                    listB.RemoveAt(i);
                }
            }
        }
    }

    public class SecondBoneAttack : BaseWhiteAttackPattern
    {
        public SecondBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base (player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 12) return;
            if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
            {
                float yOffset = up ? -90 : 90;
                AddAttack(listA, new Vector2(160, Engine.center.y + yOffset), Vector2.right * 10);
                up = !up;
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.x > 880)
                {
                    factory.ReturnAttack(listA[i]);
                    listA.RemoveAt(i);
                }
            }
        }
    }

    public class ThirdBoneAttack : BaseWhiteAttackPattern
    {
        public ThirdBoneAttack (Player player, Enemy enemy, IAbstractFactory factory) : base (player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 21 && listA.Count == 0)
            {
                selectPosition = 0;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 21) return;
            if (counter > 0.3)
            {
                float xPos = 200 + 125 * selectPosition;
                AddAttack(listA, new Vector2(xPos, Engine.center.y - 100), Vector2.down * 10);
                selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.y > 500)
                {
                    factory.ReturnAttack(listA[i]);
                    listA.RemoveAt(i);
                }
            }
        }
    }

    public class FourthBoneAttack : BaseWhiteAttackPattern
    {
        public FourthBoneAttack (Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 12) return;
            if (counter > 0.4)
            {
                float yOffset = up ? -90 : 90;
                AddAttack(listA, new Vector2(880, Engine.center.y + yOffset), Vector2.left * 10);
                up = !up;
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.x < 160)
                {
                    factory.ReturnAttack(listA[i]);
                    listA.RemoveAt(i);
                }
            }
        }
    }

    public class FifthBoneAttack : BaseWhiteAttackPattern
    {
        public FifthBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >=21 && listA.Count == 0)
            {
                selectPosition = 0;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 21) return;
            if (counter > 0.3)
            {
                float xPos = 840 - 125 * selectPosition;
                AddAttack(listA, new Vector2(xPos, Engine.center.y + 100), Vector2.up * 10);
                selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.y < 160)
                {
                    factory.ReturnAttack(listA[i]);
                    listA.RemoveAt(i);
                }
            }
        }
    }

    public class FirstBlueBoneAttack : BaseBlueAttackPattern
    {
        public FirstBlueBoneAttack(Player player, Enemy enemy, IAbstractFactory factory, IAbstractFactory factory2) : base(player, enemy, factory, factory2) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 16 && listA.Count == 0 && listB.Count == 0)
            {
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 16) return;
            if (counter > 1.2f - (duration / 20) && numOfAttacks % 2 == 0) //If 1 - (duration/20)" have passed since last attack was thrown and we have to throw a white attack:
            {
                AddAttack(listA, new Vector2(160, Engine.center.y + 90), Vector2.right * 5, mainFactory);
                AddAttack(listB, new Vector2(880, Engine.center.y - 90), Vector2.left * 5, mainFactory);
                counter = 0; //Reset attack timer.
                numOfAttacks ++;
            }
            else if (counter > 0.5f && numOfAttacks % 2 != 0) //If 0.5" have passed since last attack was thrown and we have to throw a blue attack:
            {
                AddAttack(listA, new Vector2(160, Engine.center.y + 90), Vector2.right * 5, secondaryFactory);
                AddAttack(listB, new Vector2(880, Engine.center.y - 90), Vector2.left * 5, secondaryFactory);
                counter = 0; //Reset attack timer.
                numOfAttacks++;
            }
        }

        public override void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            foreach (BaseBoneAttack attack in attackList)
            {
                Vector2 currentDir = attack.speed.normalized;
                attack.UpdateSpeed(currentDir * (3 + duration));
                attack.Update();
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            for (int i = listA.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listA[i].transform.position.x > 880)
                {
                    ReturnToCorrectFactory(listA[i]);
                    listA.RemoveAt(i);
                }
            }

            for (int i = listB.Count - 1; i >= 0; i--) //Use reverse for loop to be able to modify list while iterating it.
            {
                if (listB[i].transform.position.x < 160)
                {
                    ReturnToCorrectFactory(listB[i]);
                    listB.RemoveAt(i);
                }
            }
        }

        private void ReturnToCorrectFactory(BaseBoneAttack attack)
        {
            if (attack is BlueBoneAttack)
                mainFactory.ReturnAttack(attack);
            else if (attack is OrangeBoneAttack)
                secondaryFactory.ReturnAttack(attack);
            else
                throw new InvalidOperationException("Invalid attack type."); //What is a white bone attack doing here?
        }
    }
}
