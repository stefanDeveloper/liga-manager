﻿using LigaManagerServer.Models;

namespace LigaManagerBettorClient.Models
{
    public class RankedBettor
    {
        public Bettor Bettor { get; set; }
        public int Place { get; set; }
        public int Score { get; set; }
    }
}