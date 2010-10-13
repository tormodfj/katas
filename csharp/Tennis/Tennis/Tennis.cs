using System;

namespace Tennis
{
    public class Tennis
    {
        private string _playerOneName;
        private string _playerTwoName;

        private int _playerOneScore;
        private int _playerTwoScore;

        public event EventHandler ScoreChanged;
        protected virtual void OnScoreChanged()
        {
            if (ScoreChanged != null)
            {
                ScoreChanged(this, EventArgs.Empty);
            }
        }

        public Tennis(string playerOneName, string playerTwoName)
        {
            this._playerOneName = playerOneName;
            this._playerTwoName = playerTwoName;
        }

        public void ScorePointToPlayerOne()
        {
            _playerOneScore++;
            OnScoreChanged();
        }

        public void ScorePointToPlayerTwo()
        {
            _playerTwoScore++;
            OnScoreChanged();
        }

        public string Score
        {
            get
            {
                return CallScore();
            }
        }

        private string CallScore()
        {
            if (ScoreIsEqual)
            {
                return CallEqualScore();
            }
            else if (OnePlayerHasMoreThanForty)
            {
                return CallLateScore();
            }
            else
            {
                return CallEarlyScore();
            }
        }

        private string CallEqualScore()
        {
            if (OnePlayerHasFortyOrMore)
            {
                return "Deuce";
            }
            else
            {
                return string.Format("{0} all", CallPointScore(_playerOneScore));
            }
        }

        private string CallEarlyScore()
        {
            if (OnePlayerHasFortyOrMore)
            {
                return string.Format("{0}, {1}. Set ball {2}", CallPointScore(_playerOneScore), CallPointScore(_playerTwoScore), LeadingPlayer);
            }
            else
            {
                return string.Format("{0}, {1}", CallPointScore(_playerOneScore), CallPointScore(_playerTwoScore));
            }
        }

        private string CallLateScore()
        {
            if (OnePlayerLeadsByTwoOrMore)
            {
                return string.Format("{0} wins!", LeadingPlayer);
            }
            else
            {
                return string.Format("Advantage {0}", LeadingPlayer);
            }
        }

        private string CallPointScore(int points)
        {
            switch (points)
            {
                case 0:
                    return "Love";
                case 1:
                    return "15";
                case 2: 
                    return "30";
                case 3: 
                    return "40";
                default: 
                    return string.Empty;
            }
        }

        private bool ScoreIsEqual
        {
            get
            {
                return _playerOneScore == _playerTwoScore;
            }
        }

        private bool OnePlayerHasMoreThanForty
        {
            get
            {
                return _playerOneScore > 3 || _playerTwoScore > 3;
            }
        }

        private bool OnePlayerLeadsByTwoOrMore
        {
            get
            {
                return Math.Abs(_playerOneScore - _playerTwoScore) > 1;
            }
        }

        private bool OnePlayerHasFortyOrMore
        {
            get
            {
                return _playerOneScore >= 3 || _playerTwoScore >= 3;
            }
        }

        private string LeadingPlayer
        {
            get
            {
                if (_playerOneScore > _playerTwoScore)
                {
                    return _playerOneName;
                }
                else if (_playerOneScore < _playerTwoScore)
                {
                    return _playerTwoName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
