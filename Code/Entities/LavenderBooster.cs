using Celeste.Mod.Entities;
using Celeste.Mod.VortexHelper.Misc;
using Celeste.Mod.VortexHelper.Misc.Extensions;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using System;

namespace Celeste.Mod.VortexHelper.Entities;

[CustomEntity("VortexHelper/LavenderBooster")]
[TrackedAs(typeof(Booster))]
public class LavenderBooster : Booster
{
    public static readonly ParticleType P_BurstLavender = new(P_Burst);
    public static readonly ParticleType P_BurstExplodeLavender = new(P_Burst);

    private readonly DynData<Booster> boosterData;
    private readonly bool QoL;
    private readonly bool LegacyGravityHelper;
    
    public LavenderBooster(EntityData data, Vector2 offset)
        : base(data.Position + offset, red: false)
    {
        this.boosterData = new DynData<Booster>(this);
        QoL = data.Bool("QoL", false);
        
        // Unlike most legacy settings, this defaults to false so that old maps used the "fixed" functionality.
        // If an older map specifically wants the jank interaction, they will need to replace the entities and rerelease.
        LegacyGravityHelper = data.Bool("legacyGravityHelper", false);

        Sprite oldSprite = this.boosterData.Get<Sprite>("sprite");
        Remove(oldSprite);
        Add((Sprite) (this.boosterData["sprite"] = VortexHelperModule.LavenderBoosterSpriteBank.Create("lavenderBooster")));

        this.boosterData["particleType"] = P_BurstLavender;
    }

    public static void InitializeParticles()
    {
        P_BurstLavender.Color = Calc.HexToColor("6a38b0");

        P_BurstExplodeLavender.Color = P_BurstLavender.Color;
        P_BurstExplodeLavender.SpeedMax = 250;
    }

    internal static class Hooks
    {
        public static void Hook()
        {
            //On.Celeste.Player.DashEnd += Player_DashEnd;
            On.Celeste.Booster.PlayerReleased += Booster_PlayerReleased;
            On.Celeste.Booster.AppearParticles += Booster_AppearParticles;
        }

        public static void Unhook()
        {
            //On.Celeste.Player.DashEnd -= Player_DashEnd;
            On.Celeste.Booster.PlayerReleased -= Booster_PlayerReleased;
            On.Celeste.Booster.AppearParticles -= Booster_AppearParticles;
        }

        private static void Booster_PlayerReleased(On.Celeste.Booster.orig_PlayerReleased orig, Booster self)
        {
            orig(self);
            if (Util.TryGetPlayer(out Player player) && player.LastBooster is LavenderBooster l)
            {
                Audio.Play(SFX.game_05_redbooster_end, player.Center);
                PurpleBooster.LaunchPlayerParticles(player, player.DashDir, P_BurstExplodeLavender);
                VortexHelperModule.SessionProperties.BoosterQoL = false;
                VortexHelperModule.SessionProperties.BoosterLegacyGravityHelper = l.LegacyGravityHelper;
                PurpleBooster.PurpleBoosterExplodeLaunch(player, self.Center - player.DashDir, null, -1f);
            }
        }

        private static void Booster_AppearParticles(On.Celeste.Booster.orig_AppearParticles orig, Booster self)
        {
            if (self is LavenderBooster)
            {
                ParticleSystem particlesBG = self.SceneAs<Level>().ParticlesBG;
                for (int i = 0; i < 360; i += 30)
                    particlesBG.Emit(PurpleBooster.P_Appear, 1, self.Center, Vector2.One * 2f, i * ((float) Math.PI / 180f));
            }
            else
                orig(self);
        }
    }
}
