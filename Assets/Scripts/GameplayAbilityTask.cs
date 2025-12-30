namespace WYGAS
{
    public abstract class GameplayAbilityTask
    {
        private GameplayAbility ability;
        private AbilitySystem sourceAS;

        private bool isActive = false;
        private bool isEnded = false;
        
        public void Initialize(GameplayAbility inAbility)
        {
            ability = inAbility;
        } 
        
        public virtual void Tick(float deltaTime) {}

        public bool IsActive()
        {
            return isActive;
        }
        
        public void Activate()
        {
            isActive = true;
            OnActivate();
        }

        public void EndTask()
        {
            if (isEnded) return;
            isActive = false;
            isEnded = true;
        }

        protected abstract void OnActivate();
    }
}