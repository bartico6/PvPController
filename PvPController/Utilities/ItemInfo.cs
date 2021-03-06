﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvPController.Utilities {
    public class ItemInfo {
        public int damage { get; set; }
        public int defense { get; set; }

        public BuffDuration debuff { get; set; }
        public BuffDuration selfBuff { get; set; }

        public ItemInfo() {
            damage = 0;
            defense = 0;

            debuff = new BuffDuration(0, 0);
            selfBuff = new BuffDuration(0, 0);
        }
    }

    public class BuffDuration {
        public int buffid { get; set; }
        public int buffDuration { get; set; }

        public BuffDuration(int buffid, int buffDuration) {
            this.buffid = buffid;
            this.buffDuration = buffDuration;
        }
    }
}
