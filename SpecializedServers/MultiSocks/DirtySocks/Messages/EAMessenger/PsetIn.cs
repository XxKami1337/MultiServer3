﻿namespace MultiSocks.DirtySocks.Messages
{
    public class PsetIn : AbstractMessage
    {
        public override string _Name { get => "PSET"; }
        public string? RSRC { get; set; }
        public string? SHOW { get; set; }
        public string? PROD { get; set; }
        public string? STAT { get; set; }

        public override void Process(AbstractDirtySockServer context, DirtySockClient client)
        {
            client.SendMessage(new PsetOut()
            {
                SHOW = SHOW,
                PROD = PROD,
                STAT = STAT,
            });
        }
    }
}
