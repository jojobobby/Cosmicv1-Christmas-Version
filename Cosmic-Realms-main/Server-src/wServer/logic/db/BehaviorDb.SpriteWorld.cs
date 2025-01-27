﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
//by GhostMaree
namespace wServer.logic
{
	partial class BehaviorDb
	{
		private _ SpriteWorld = () => Behav()
			.Init("Native Fire Sprite",
				new State(
                    new Prioritize(
                        new StayAbove(speed: 0.4, altitude: 95),
                        new Shoot(radius: 10, count: 2, shootAngle: 7, projectileIndex: 0, coolDown: 300)
                    ),
                    new Wander(speed: 1.4)
				),
				new Threshold(0.01,
                    new TierLoot(tier: 5, type: ItemType.Weapon, probability: 0.4),
                    new ItemLoot(item: "Magic Potion", probability: 0.05)
				)
			)
            .Init("Native Ice Sprite",
				new State(
                    new Prioritize(
                        new StayAbove(speed: 0.4, altitude: 105),
                        new Wander(speed: 1.4)
                    ),
                    new Shoot(radius: 10, count: 3, shootAngle: 7, projectileIndex: 0, coolDown: 1000)
                ),
                new Threshold(0.01,
					new TierLoot(tier: 2, type: ItemType.Ability, probability: 0.4),
                    new ItemLoot(item: "Magic Potion", probability: 0.05)
                )
            )
            .Init("Native Magic Sprite",
				new State(
                    new Prioritize(
                        new StayAbove(speed: 0.4, altitude: 115),
                        new Wander(speed: 1.4)
                    ),
                    new Shoot(radius: 10, count: 4, shootAngle: 7, projectileIndex: 0, coolDown: 1000)
                ),
                new Threshold(0.01,
					new TierLoot(tier: 6, type: ItemType.Armor, probability: 0.4),
                    new ItemLoot(item: "Magic Potion", probability: 0.05)
                )
            )
            .Init("Native Nature Sprite",
				new State(
                    new Shoot(radius: 10, count: 5, shootAngle: 7, projectileIndex: 0, coolDown: 1000),
                    new Wander(speed: 1.6)
                ),
                new Threshold(0.01,
                    new ItemLoot(item: "Magic Potion", probability: 0.015),
                    new ItemLoot(item: "Sprite Wand", probability: 0.015),
                    new ItemLoot(item: "Ring of Greater Magic", probability: 0.4)
                )
            )
            .Init("Native Darkness Sprite",
				new State(
					new Shoot(radius: 10, count: 5, shootAngle: 7, projectileIndex: 0, coolDown: 1000),
                    new Wander(speed: 1.6)
                ),
                new Threshold(0.01,
					new ItemLoot(item: "Health Potion", probability: 0.015),
                    new ItemLoot(item: "Ring of Dexterity", probability: 0.4)
                )
            )
            .Init("Native Sprite God",
				new State(
                    new Prioritize(
                        new StayAbove(speed: 0.4, altitude: 200),
                        new Wander(speed: 0.4)
                    ),
                    new Shoot(radius: 12, count: 4, shootAngle: 10, projectileIndex: 0, coolDown: 1000),
                    new Shoot(radius: 12, projectileIndex: 1, predictive: 1, coolDown: 1000)
                ),
                new Threshold(0.01,
                    new TierLoot(tier: 6, type: ItemType.Weapon, probability: 0.4),
                    new TierLoot(tier: 7, type: ItemType.Weapon, probability: 0.25),
                    new TierLoot(tier: 8, type: ItemType.Weapon, probability: 0.25),
                    new TierLoot(tier: 7, type: ItemType.Armor, probability: 0.4),
                    new TierLoot(tier: 8, type: ItemType.Armor, probability: 0.25),
                    new TierLoot(tier: 9, type: ItemType.Armor, probability: 0.25),
                    new TierLoot(tier: 4, type: ItemType.Ring, probability: 0.125),
                    new TierLoot(tier: 4, type: ItemType.Ability, probability: 0.125),
                    new ItemLoot(item: "Potion of Attack", probability: 0.1)
                )
            )
            .Init("Limon the Sprite God",
				new State(
                   new ScaleHP2(40,3,15),
                   new ChangeMusic("https://github.com/GhostRealm/GhostRealm.github.io/raw/master/music/Sprite.mp3"),
                    new State("start_the_fun",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new PlayerWithinTransition(dist: 11, targetState: "begin_teleport1", seeInvis: true)
                    ),
                    new State("begin_teleport1",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Prioritize(
                            new StayCloseToSpawn(speed: 0.5, range: 7),
                            new Wander(speed: 0.5)
                        ),
                        new Flash(color: 0x00FF00, flashPeriod: 0.25, flashRepeats: 8),
                        new TimedTransition(time: 2000, targetState: "teleport1")
                    ),
                    new State("teleport1",
                        new Prioritize(
                            new StayCloseToSpawn(speed: 1.6, range: 7),
                            new Follow(speed: 6, acquireRange: 10, range: 2),
                            new Follow(speed: 0.3, acquireRange: 10, range: 0.2)
                        ),
                        new TimedTransition(time: 300, targetState: "circle_player")
                    ),
                    new State("circle_player",
                        new Shoot(radius: 8, count: 2, shootAngle: 10, projectileIndex: 0, angleOffset: 0.7, predictive: 0.4, coolDown: 400),
                        new Shoot(radius: 8, count: 2, shootAngle: 180, projectileIndex: 0, angleOffset: 0.7, predictive: 0.4, coolDown: 400),
                        new Prioritize(
                            new StayCloseToSpawn(speed: 1.3, range: 7),
                            new Orbit(speed: 1.8, radius: 4, acquireRange: 5, target: null),
                            new Follow(speed: 6, acquireRange: 10, range: 2),
                            new Follow(speed: 0.3, acquireRange: 10, range: 0.2)
                        ),
                        new State("check_if_not_moving",
                            new NotMovingTransition(targetState: "boom", delay: 500)
                        ),
                        new State("boom",
                            new Shoot(radius: 8, count: 18, shootAngle: 20, projectileIndex: 0, angleOffset: 0.4, predictive: 0.4, coolDown: 1500),
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new TimedTransition(time: 1000, targetState: "check_if_not_moving")
                        ),
                        new TimedTransition(time: 10000, targetState: "set_up_the_box")
                    ),
                    new State("set_up_the_box",
                        new TossObject(child: "Limon Element 1", range: 9.5, angle: 315, coolDown: 1000000),
                        new TossObject(child: "Limon Element 2", range: 9.5, angle: 225, coolDown: 1000000),
                        new TossObject(child: "Limon Element 3", range: 9.5, angle: 135, coolDown: 1000000),
                        new TossObject(child: "Limon Element 4", range: 9.5, angle: 45, coolDown: 1000000),
                        new TossObject(child: "Limon Element 1", range: 14, angle: 315, coolDown: 1000000),
                        new TossObject(child: "Limon Element 2", range: 14, angle: 225, coolDown: 1000000),
                        new TossObject(child: "Limon Element 3", range: 14, angle: 135, coolDown: 1000000),
                        new TossObject(child: "Limon Element 4", range: 14, angle: 45, coolDown: 1000000),
                        new State("shielded1",
                            new Shoot(radius: 8, count: 1, predictive: 0.1, coolDown: 1000),
                            new Shoot(radius: 8, count: 3, shootAngle: 120, angleOffset: 0.3, predictive: 0.1, coolDown: 500),
                            new TimedTransition(1500, targetState: "shielded2")
                        ),
                        new State("shielded2",
                            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                            new Shoot(radius: 8, count: 3, shootAngle: 120, angleOffset: 0.3, predictive: 0.2, coolDown: 800),
                            new TimedTransition(3500, targetState: "shielded1")
                        ),
                        new TimedTransition(time: 20000, targetState: "Summon_the_sprites")
                    ),
                    new State("Summon_the_sprites",
                        new StayCloseToSpawn(speed: 0.5, range: 8),
                        new Wander(speed: 0.5),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(radius: 8, count: 3, shootAngle: 15, coolDown: 1300),
                        new Spawn(children: "Magic Sprite", maxChildren: 2, initialSpawn: 0, coolDown: 500),
                        new Spawn(children: "Ice Sprite", maxChildren: 1, initialSpawn: 0, coolDown: 500),
                        new TimedTransition(time: 11000, targetState: "begin_teleport1"),
                        new HpLessTransition(threshold: 0.2, targetState: "begin_teleport1")
                    ),
                    new DropPortalOnDeath(target: "Epic Sprite World Portal", probability: 1)
                ),
                new Threshold(0.0001,
                    new ItemLoot(item: "Dagger of the Endless Magic", probability: 0.002, damagebased: true),
                    new ItemLoot(item: "Wand of Mythical Fusion", probability: 0.002, damagebased: true),

                    new TierLoot(tier: 6, type: ItemType.Armor, probability: 0.4),
                    new TierLoot(tier: 7, type: ItemType.Armor, probability: 0.4),
                    new TierLoot(tier: 3, type: ItemType.Ability, probability: 0.25),
                    new TierLoot(tier: 4, type: ItemType.Ability, probability: 0.125),
                    new TierLoot(tier: 5, type: ItemType.Ability, probability: 0.0625),
                    new TierLoot(tier: 3, type: ItemType.Ring, probability: 0.25),
                    new ItemLoot(item: "Potion of Dexterity", probability: 0.3, numRequired: 3),
                    new ItemLoot(item: "Potion of Defense", probability: 0.1),
                    new ItemLoot(item: "Sprite Wand", probability: 0.004, 0, 0.03),
                    new ItemLoot(item: "Wine Cellar Incantation", probability: 0.05),
                    new ItemLoot("Mark of Limon", 1),
                        new ItemLoot(item: "Sprite World Key", 0.02),
                        new ItemLoot("Weirdly Pulsating Armor", 0.001, damagebased: true),
                         new ItemLoot(item: "Sprite Essence", probability: 0.0045, threshold: 0.01, damagebased: true),
                    new ItemLoot(item: "Spriteful Dirk", probability: 0.006, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Awoken Spriteful Dirk", probability: 0.002, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Staff of Extreme Prejudice", probability: 0.006, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Cloak of the Planewalker", probability: 0.006, damagebased: true, threshold: 0.01)
                )
            )
            .Init("Limon Element 1",
				new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition(target: "Limon the Sprite God", dist: 999, targetState: "Suicide"),
					new State("Setup",
                        new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                    new State("Attacking1",
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                    new State("Attacking2",
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 135, defaultAngle: 135, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking3")      
                    ),
                    new State("Attacking3",
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                    new State("Suicide",
                        new Suicide()
                    ),
                    new Decay(time: 20000)
                )
            )
            .Init("Limon Element 2",
				new State(
					new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition(target: "Limon the Sprite God", dist: 999, targetState: "Suicide"),
                    new State("Setup",
						new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                    new State("Attacking1",
						new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                    new State("Attacking2",
						new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 45, defaultAngle: 45, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                    new State("Attacking3",
						new Shoot(radius: 999, fixedAngle: 90, defaultAngle: 90, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                    new State("Suicide",
						new Suicide()
                    ),
                    new Decay(time: 20000)
                )
            )
            .Init("Limon Element 3",
				new State(
					new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition(target: "Limon the Sprite God", dist: 999, targetState: "Suicide"),
                    new State("Setup",
						new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                    new State("Attacking1",
						new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                    new State("Attacking2",
						new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 315, defaultAngle: 315, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                    new State("Attacking3",
						new Shoot(radius: 999, fixedAngle: 0, defaultAngle: 0, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                    new State("Suicide",
						new Suicide()
                    ),
                    new Decay(time: 20000)
                )
            )
            .Init("Limon Element 4",
				new State(
					new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new EntityNotExistsTransition(target: "Limon the Sprite God", dist: 999, targetState: "Suicide"),
                    new State("Setup",
						new TimedTransition(time: 2000, targetState: "Attacking1")
                    ),
                    new State("Attacking1",
						new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking2")
                    ),
                    new State("Attacking2",
						new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 225, defaultAngle: 225, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Attacking3")
                    ),
                    new State("Attacking3",
						new Shoot(radius: 999, fixedAngle: 270, defaultAngle: 270, coolDown: 300),
                        new Shoot(radius: 999, fixedAngle: 180, defaultAngle: 180, coolDown: 300),
                        new TimedTransition(time: 6000, targetState: "Setup")
                    ),
                    new State("Suicide",
						new Suicide()
                    ),
                    new Decay(time: 20000)
                )
            )
         /* .Init("Epic Limon the Sprite God",
            new State(
                new DropPortalOnDeath("Realm Portal"),
                new ScaleHP(4000, 4000 * 30),
                    new State("1",
                        new Wander(1),
                        new Shoot(50, count: 8, shootAngle: 60, projectileIndex: 0, predictive: 1, coolDown: 1250),
                        new TimedTransition(11750, "2")
                        ),
                      new State("2",
                          new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(50, count: 8, shootAngle: 90, projectileIndex: 0, predictive: 1, coolDown: 1250),
                         new Shoot(80, count: 4, shootAngle: 90, projectileIndex: 0, predictive: 4, coolDown: 1250),
                        new TimedTransition(3750, "3")
                          ),
                      new State("3",
                        new Shoot(80, count: 4, shootAngle: 90, projectileIndex: 0, predictive: 4, coolDown: 1250),
                        new Shoot(85, count: 4, shootAngle: 90, projectileIndex: 0, predictive: 2, coolDown: 2250),
                        new Shoot(90, count: 4, shootAngle: 90, projectileIndex: 0, predictive: 1, coolDown: 4250),
                        new TimedTransition(10750, "4")
                            )
                         ),
                   new Threshold(0.01,
                    new TierLoot(tier: 6, type: ItemType.Armor, probability: 0.4),
                    new TierLoot(tier: 7, type: ItemType.Armor, probability: 0.4),
                    new TierLoot(tier: 3, type: ItemType.Ability, probability: 0.25),
                    new TierLoot(tier: 4, type: ItemType.Ability, probability: 0.125),
                    new TierLoot(tier: 5, type: ItemType.Ability, probability: 0.0625),
                    new TierLoot(tier: 3, type: ItemType.Ring, probability: 0.25),
                    new ItemLoot(item: "Potion of Dexterity", probability: 0.3, numRequired: 3),
                     new ItemLoot(item: "Potion of Dexterity", probability: 1.3, numRequired: 3),
                      new ItemLoot(item: "Potion of Dexterity", probability: 1.3, numRequired: 3),
                       new ItemLoot(item: "Potion of Dexterity", probability: 0.3, numRequired: 3),
                    new ItemLoot(item: "Potion of Defense", probability: 0.1),
                    new ItemLoot(item: "Sprite Wand", probability: 0.004, 0, 0.03),
                    new ItemLoot(item: "Wine Cellar Incantation", probability: 0.05),
                    new ItemLoot("Mark of Limon", 0, 1),//Sprite Essence
                        new ItemLoot(item: "Sprite World Key", 0.02),
                        new ItemLoot("50 Credits", 0.01),
                     new ItemLoot(item: "Sprite Essence", probability: 0.01, threshold: 0.01),
                    new ItemLoot(item: "Spriteful Dirk", probability: 0.015, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Awoken Spriteful Dirk", probability: 0.006, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Staff of Extreme Prejudice", probability: 0.015, damagebased: true, threshold: 0.01),
                    new ItemLoot(item: "Cloak of the Planewalker", probability: 0.015, damagebased: true, threshold: 0.01)
                )
            )*/
                    
        
            ;
	}
}