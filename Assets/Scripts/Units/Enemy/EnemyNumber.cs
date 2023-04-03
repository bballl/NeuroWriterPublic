namespace GameBoxProject
{
    class EnemyNumber : Enemy
    {
        protected override void Dead(object obj)
        {
            int exp = 0;
            if (_enemyModel.LetterData.Name == '0')
                exp = _data.NullNumberExperience;
            else
                exp = _data.OneNumberExperience;

            DropController.CreateDrop<DroppedExp>(exp, transform.position);
            base.Dead(obj);
        }
    }
}
