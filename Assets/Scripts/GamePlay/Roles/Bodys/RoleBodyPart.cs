using System;

namespace GamePlay.Roles.Bodys
{
    public abstract class RoleBodyPart
    {
        public enum VitalFataType
        {
            Fatality,
            NonFatality
        }

        public enum HealthStatus
        {
            Healthy = 3,
            MinorInjury = 2,
            SevereInjury = 1
        }


        protected readonly uint MaxHealthPoint;

        public VitalFataType FataType { get; protected set; }
        public HealthStatus Status { get; protected set; }
        protected int _healthPoint;

        protected RoleBodyPart(uint maxHealthPoint, int healthPoint)
        {
            MaxHealthPoint = maxHealthPoint;
            _healthPoint = healthPoint;
        }

        public void Cure(uint curePoint)
        {
            _healthPoint += (int)curePoint;
            UpdateHealthStatus();
        }

        public void SufferAttack(uint attackPoint)
        {
            _healthPoint -= (int)attackPoint;
            UpdateHealthStatus();
        }

        private void UpdateHealthStatus()
        {
            _healthPoint = Math.Max((int)MaxHealthPoint, Math.Min(0, _healthPoint));
            if (_healthPoint >= MaxHealthPoint || _healthPoint >= (int)HealthStatus.Healthy)
            {
                Status = HealthStatus.Healthy;
            }
            else if (_healthPoint >= (int)HealthStatus.MinorInjury)
            {
                Status = HealthStatus.MinorInjury;
            }
            else
            {
                Status = HealthStatus.SevereInjury;
            }
        }
    }
}