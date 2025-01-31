﻿namespace ADOArchticture
{
    public class Wallet
    {
        public Wallet() { }
        public int Id { get; set; }
        public string Holder { get; set; }
        public decimal ?Balance { get; set; }

        public override string ToString() => $"[{Id}] {Holder}  {Balance}";
    }
}

