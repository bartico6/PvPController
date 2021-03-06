﻿using Microsoft.Xna.Framework;
using PvPController.PvPVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace PvPController.Utilities {
    class TerrariaUtils {
        public static double GetHurtDamage(PvPPlayer damagedPlayer, int Damage) {
            damagedPlayer.TPlayer.stealth = 1f;
            int Damage1 = Damage;
            double dmg = Main.CalculatePlayerDamage(Damage1, damagedPlayer.TPlayer.statDefense);
            if (dmg >= 1.0) {
                dmg = (double)(int)((1.0 - (double)damagedPlayer.TPlayer.endurance) * dmg);
                if (dmg < 1.0)
                    dmg = 1.0;
                if (damagedPlayer.TPlayer.ConsumeSolarFlare()) {
                    dmg = (double)(int)(0.7 * dmg);
                    if (dmg < 1.0)
                        dmg = 1.0;
                }
                if (damagedPlayer.TPlayer.beetleDefense && damagedPlayer.TPlayer.beetleOrbs > 0) {
                    dmg = (double)(int)((1.0 - (double)(0.15f * (float)damagedPlayer.TPlayer.beetleOrbs)) * dmg);
                    damagedPlayer.TPlayer.beetleOrbs = damagedPlayer.TPlayer.beetleOrbs - 1;
                    if (dmg < 1.0)
                        dmg = 1.0;
                }
                if (damagedPlayer.TPlayer.defendedByPaladin) {
                    if (damagedPlayer.TPlayer.whoAmI != Main.myPlayer) {
                        if (Main.player[Main.myPlayer].hasPaladinShield) {
                            Player player = Main.player[Main.myPlayer];
                            if (player.team == damagedPlayer.TPlayer.team && damagedPlayer.TPlayer.team != 0) {
                                float num1 = player.Distance(damagedPlayer.TPlayer.Center);
                                bool flag3 = (double)num1 < 800.0;
                                if (flag3) {
                                    for (int index = 0; index < (int)byte.MaxValue; ++index) {
                                        if (index != Main.myPlayer && Main.player[index].active && (!Main.player[index].dead && !Main.player[index].immune) && (Main.player[index].hasPaladinShield && Main.player[index].team == damagedPlayer.TPlayer.team && (double)Main.player[index].statLife > (double)Main.player[index].statLifeMax2 * 0.25)) {
                                            float num2 = Main.player[index].Distance(damagedPlayer.TPlayer.Center);
                                            if ((double)num1 > (double)num2 || (double)num1 == (double)num2 && index < Main.myPlayer) {
                                                flag3 = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (flag3) {
                                    int Damage2 = (int)(dmg * 0.25);
                                    dmg = (double)(int)(dmg * 0.75);
                                    player.Hurt(PlayerDeathReason.LegacyEmpty(), Damage2, 0, false, false, false, -1);
                                }
                            }
                        }
                    } else {
                        bool flag3 = false;
                        for (int index = 0; index < (int)byte.MaxValue; ++index) {
                            if (index != Main.myPlayer && Main.player[index].active && (!Main.player[index].dead && !Main.player[index].immune) && (Main.player[index].hasPaladinShield && Main.player[index].team == damagedPlayer.TPlayer.team && (double)Main.player[index].statLife > (double)Main.player[index].statLifeMax2 * 0.25)) {
                                flag3 = true;
                                break;
                            }
                        }
                        if (flag3)
                            dmg = (double)(int)(dmg * 0.75);
                    }
                }
            }
            return dmg;
        }

        public static int GetWeaponDamage(PvPPlayer attacker, PvPItem weapon) {
            int dmg = GetPrefixDamage(weapon);

            if (dmg > 0) {
                if (weapon.melee)
                    dmg = (int)(dmg * (double)attacker.TPlayer.meleeDamage);
                else if (weapon.ranged) {
                    dmg = (int)(dmg * (double)attacker.TPlayer.rangedDamage);
                    if (weapon.useAmmo == AmmoID.Arrow || weapon.useAmmo == AmmoID.Stake)
                        dmg = (int)(dmg * (double)attacker.TPlayer.arrowDamage);
                    if (weapon.useAmmo == AmmoID.Bullet || weapon.useAmmo == AmmoID.CandyCorn)
                        dmg = (int)(dmg * (double)attacker.TPlayer.bulletDamage);
                    if (weapon.useAmmo == AmmoID.Rocket || weapon.useAmmo == AmmoID.StyngerBolt || (weapon.useAmmo == AmmoID.JackOLantern || weapon.useAmmo == AmmoID.NailFriendly))
                        dmg = (int)(dmg * (double)attacker.TPlayer.rocketDamage);
                } else if (weapon.thrown)
                    dmg = (int)(dmg * (double)attacker.TPlayer.thrownDamage);
                else if (weapon.magic)
                    dmg = (int)(dmg * (double)attacker.TPlayer.magicDamage);
                else if (weapon.summon)
                    dmg = (int)(dmg * (double)attacker.TPlayer.minionDamage);
            }
            return dmg;
        }

        public static int GetPrefixDamage(PvPItem weapon) {
            int prefix = weapon.prefix;

            float damage = 1f;

            if (prefix == 3)
                damage = 1.05f;
            else if (prefix == 4)
                damage = 1.1f;
            else if (prefix == 5)
                damage = 1.15f;
            else if (prefix == 6)
                damage = 1.1f;
            else if (prefix == 81)
                damage = 1.15f;
            else if (prefix == 8)
                damage = 0.85f;
            else if (prefix == 10)
                damage = 0.85f;
            else if (prefix == 12)
                damage = 1.05f;
            else if (prefix == 13)
                damage = 0.9f;
            else if (prefix == 16)
                damage = 1.1f;
            else if (prefix == 20)
                damage = 1.1f;
            else if (prefix == 21)
                damage = 1.1f;
            else if (prefix == 82)
                damage = 1.15f;
            else if (prefix == 22)
                damage = 0.85f;
            else if (prefix == 25)
                damage = 1.15f;
            else if (prefix == 58)
                damage = 0.85f;
            else if (prefix == 26)
                damage = 1.1f;
            else if (prefix == 28)
                damage = 1.15f;
            else if (prefix == 83)
                damage = 1.15f;
            else if (prefix == 30)
                damage = 0.9f;
            else if (prefix == 31)
                damage = 0.9f;
            else if (prefix == 32)
                damage = 1.1f;
            else if (prefix == 34)
                damage = 1.1f;
            else if (prefix == 35)
                damage = 1.15f;
            else if (prefix == 52)
                damage = 0.9f;
            else if (prefix == 37)
                damage = 1.1f;
            else if (prefix == 53)
                damage = 1.1f;
            else if (prefix == 55)
                damage = 1.05f;
            else if (prefix == 59)
                damage = 1.15f;
            else if (prefix == 60)
                damage = 1.15f;
            else if (prefix == 39)
                damage = 0.7f;
            else if (prefix == 40)
                damage = 0.85f;
            else if (prefix == 41)
                damage = 0.9f;
            else if (prefix == 57)
                damage = 1.18f;
            else if (prefix == 43)
                damage = 1.1f;
            else if (prefix == 46)
                damage = 1.07f;
            else if (prefix == 50)
                damage = 0.8f;
            else if (prefix == 51)
                damage = 1.05f;

            return (int)Math.Round(PvPController.config.itemInfo[weapon.netID].damage * (double)damage);
        }

        public static void ActivateYoyo(PvPPlayer attacker, PvPPlayer target, int dmg, float kb) {
            if (!attacker.TPlayer.yoyoGlove && attacker.TPlayer.counterWeight <= 0)
                return;
            int index1 = -1;
            int num1 = 0;
            int num2 = 0;
            for (int index2 = 0; index2 < 1000; ++index2) {
                if (Main.projectile[index2].active && Main.projectile[index2].owner == attacker.TPlayer.whoAmI) {
                    if (Main.projectile[index2].counterweight)
                        ++num2;
                    else if (Main.projectile[index2].aiStyle == 99) {
                        ++num1;
                        index1 = index2;
                    }
                }
            }
            if (attacker.TPlayer.yoyoGlove && num1 < 2) {
                if (index1 < 0)
                    return;
                int index;
                Vector2 vector2_1 = Vector2.Subtract(target.LastNetPosition, attacker.TPlayer.Center);
                vector2_1.Normalize();
                Vector2 vector2_2 = Vector2.Multiply(vector2_1, 16f);
                index = Projectile.NewProjectile(attacker.TPlayer.Center.X, attacker.TPlayer.Center.Y, vector2_2.X, vector2_2.Y, Main.projectile[index1].type, Main.projectile[index1].damage, Main.projectile[index1].knockBack, attacker.TPlayer.whoAmI, 1f, 0.0f);
                NetMessage.SendData(27, -1, -1, null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            } else {
                if (num2 >= num1)
                    return;
                int index;
                Vector2 vector2_1 = Vector2.Subtract(target.LastNetPosition, attacker.TPlayer.Center);
                vector2_1.Normalize();
                Vector2 vector2_2 = Vector2.Multiply(vector2_1, 16f);
                float KnockBack = (float)((kb + 6.0) / 2.0);
                if (num2 > 0)
                    index = Projectile.NewProjectile(attacker.TPlayer.Center.X, attacker.TPlayer.Center.Y, vector2_2.X, vector2_2.Y, attacker.TPlayer.counterWeight, (int)(dmg * 0.8), KnockBack, attacker.TPlayer.whoAmI, 1f, 0.0f);
                else
                    index = Projectile.NewProjectile(attacker.TPlayer.Center.X, attacker.TPlayer.Center.Y, vector2_2.X, vector2_2.Y, attacker.TPlayer.counterWeight, (int)(dmg * 0.8), KnockBack, attacker.TPlayer.whoAmI, 0.0f, 0.0f);
                NetMessage.SendData(27, -1, -1, null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
        }
    }
}
