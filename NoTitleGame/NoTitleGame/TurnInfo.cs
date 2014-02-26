using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Characters;

namespace NoTitleGame
{
    class TurnInfo
    {
        // Fields
        private Character[] players;
        private int numberOfPlayers;
        private int currentPlayer;

        // Defalut constructor
        public TurnInfo(int numberOfPlayers)
        {
            this.NumberOfPlayers = numberOfPlayers;
            players = new Character[numberOfPlayers];
        }

        // Properties
        public Character[] Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public int NumberOfPlayers
        {
            get { return this.numberOfPlayers; }
            set { this.numberOfPlayers = value; }
        }

        public int CurrentPlayer
        {
            get { return this.currentPlayer; }
        }

        // Methods
        public void SetUpPlayers(Random random)
        {
            for (int i = 0; i < players.Count(); i++)
            {
                players[i].SetOnRadnomPosition(random);
            }
        }
    }
}
