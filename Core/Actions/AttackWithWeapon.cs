using System;
using Core.Models;

namespace Core.Actions
{
    public class AttackWithWeapon
    {
        private readonly Item _weapon;
        private readonly int _minDamage;
        private readonly int _maxDamage;

        public event EventHandler<string> OnActionDo;

        public AttackWithWeapon(Item weapon, int minDamage, int maxDamage)
        {
            if (weapon.Category != Item.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} isn't a weapon!");
            }

            if (_minDamage < 0)
            {
                throw new ArgumentException("Minimum Damage must be more than 0");
            }

            if (_maxDamage < _minDamage)
            {
                throw new ArgumentException("Maximum damage must be more than minimum damage");
            }

            _weapon = weapon;
            _maxDamage = maxDamage;
            _minDamage = minDamage;
        }

        public void Execute(LivingBeing actor, LivingBeing target)
        {
            int damage = GodOfRandom.NumberBetween(_minDamage, _maxDamage);
            if (damage == 0)
            {
                ReportResult("You missed");
            }
            else
            {
                ReportResult($"You hit the {target.Name} for {damage}");
                target.TakeDamage(damage);
            }
        }

        private void ReportResult(string result)
        {
            OnActionDo?.Invoke(this, result);
        }
    }
}