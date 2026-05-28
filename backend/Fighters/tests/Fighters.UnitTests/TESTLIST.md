# TESTLIST — Fighters Unit Tests

> Список тестов для покрытия основных интерфейсов и классов проекта Fighters.
> Фреймворк: **xUnit** + **Moq**

---

### IFighter / SingleFighter

- [x] `SingleFighter_Create_PropertiesInitialized` — Name, Stats, Race, Speciality, Armor, Weapon корректно проставлены
- [x] `SingleFighter_EmptyName_ReturnsDefault` — Name = "" -> "Безымянный боец"
- [x] `SingleFighter_GetMaxHealth_CalculatesCorrectly` — str\*25 + dex\*20 + int\*15 + race bonus
- [x] `SingleFighter_GetCurrentHealth_EqualsMaxHealthOnCreation` — после создания GetCurrentHealth() == GetMaxHealth()
- [x] `SingleFighter_TakeDamage_ReducesHealth` — вызов TakeDamage(10) уменьшает здоровье на 10
- [x] `SingleFighter_TakeDamage_ClampsAtZero` — здоровье не уходит ниже 0
- [x] `SingleFighter_SetArmor_UpdatesArmor` — после SetArmor() возвращается новый armor
- [x] `SingleFighter_SetWeapon_UpdatesWeapon` — после SetWeapon() возвращается новое оружие

### IFighterTeam / FighterTeam

- [x] `FighterTeam_AddFighter_MemberAdded` — после AddFighter() GetMembers() содержит бойца
- [x] `FighterTeam_GetMembers_ReturnsAllAdded` — несколько бойцов возвращаются все

---

### IItem / ItemStats

- [x] `ItemStats_PropertiesCanBeSet` — Strength/Dexterity/Intelligence get/set работают
- [x] `ItemStats_ValidateThrowsOnNegative` — отрицательные значения → исключение

---

### IWeapon / конкретные оружия

- [ ] `Weapon_Fists_PropertiesCorrect`
- [ ] `Weapon_WoodenSword_PropertiesCorrect`
- [ ] `Weapon_Club_PropertiesCorrect`
- [ ] `Weapon_ReinforcedClub_PropertiesCorrect`
- [ ] `Weapon_HuntingKnife_PropertiesCorrect`
- [ ] `Weapon_Dagger_PropertiesCorrect`
- [ ] `Weapon_BroadSword_PropertiesCorrect`
- [ ] `Weapon_GrassBlade_PropertiesCorrect`
- [ ] `Weapon_MonoBlade_PropertiesCorrect`
- [ ] `Weapon_CeremonialKnife_PropertiesCorrect`
- [ ] `Weapon_DragonSlayer_PropertiesCorrect`
- [ ] `Weapon_TheSeparator_PropertiesCorrect`
- [ ] `Weapon_Brick_PropertiesCorrect`
- [ ] `Weapon_CrudeBow_PropertiesCorrect`
- [ ] `Weapon_Longbow_PropertiesCorrect`
- [ ] `Weapon_HeavyCrossbow_PropertiesCorrect`
- [ ] `Weapon_Revolver_PropertiesCorrect`
- [ ] `Weapon_GoldenDeagle_PropertiesCorrect`
- [ ] `Weapon_GhostRifle_PropertiesCorrect`
- [ ] `Weapon_AlchemicalConcoction_PropertiesCorrect`
- [ ] `Weapon_RocketLauncher_PropertiesCorrect`
- [ ] `Weapon_StarCannnon_PropertiesCorrect`
- [ ] `Weapon_SparkWand_PropertiesCorrect`
- [ ] `Weapon_ApprenticeWand_PropertiesCorrect`
- [ ] `Weapon_LeafWand_PropertiesCorrect`
- [ ] `Weapon_SlitherWand_PropertiesCorrect`
- [ ] `Weapon_OakStaff_PropertiesCorrect`
- [ ] `Weapon_DragonBreath_PropertiesCorrect`
- [ ] `Weapon_Nirvana_PropertiesCorrect`

### WeaponType

- [ ] `WeaponType_HasAllValues` — Melee=0, Ranged=1, Magic=2

---

### IArmor / конкретные брони

- [ ] `Armor_NoArmor_ResistancesEmpty` — Resistances пустой; ModifyWeaponDamage/ModifyIncomingDamage возвращают как есть
- [ ] `Armor_LeatherArmor_Resistances_Physic10` — Resistances содержит Physic=0.1f
- [ ] `Armor_KnightArmor_Resistances` — Physic=0.4, Poison=0.1, Magic=0.05
- [ ] `Armor_GlassArmor_DoublesDamage` — ModifyWeaponDamage и ModifyIncomingDamage удваивают урон
- [ ] `Armor_BerserkArmor_ScalesWithMissingHp` — ModifyWeaponDamage множитель растёт при потере HP
- [ ] `Armor_WitchDoctorArmor_ModifyIncomingDamage` — Physic/Pure → Magic, -20% dmg, -15% crit
- [ ] `Armor_WitchDoctorArmor_ModifyWeaponDamage` — Magic/Poison +20%, Physic/Pure → Magic +15%

### DamageType

- [ ] `DamageType_HasAllValues` — Physic=0, Magic=1, Pure=2, Poison=3

---

### DamageStats

- [ ] `DamageStats_DefaultValues` — MinDamage/MaxDamage/Type/CritChance/CritDamage = default
- [ ] `DamageStats_PropertiesCanBeSet` — все свойства get/set работают

---

### IRace / конкретные расы

- [ ] `Race_Human_StatBonus_3_3_3` — GetStatBonus → +3/+3/+3; Initiative +3
- [ ] `Race_Goblin_StatBonus` — -2 Str, +1 Dex, +3 Int; Init +1
- [ ] `Race_Goblin_ModifyWeaponDamage_RarityScaling` — корректирует урон в зависимости от Rarity
- [ ] `Race_Drow_StatBonus` — -1 Str, +5 Dex, +1 Int; Init +4
- [ ] `Race_Drow_ModifyWeaponDamage_RangedBonus` — +50 damage у Ranged оружия
- [ ] `Race_Gnome_StatBonus` — +4 Str, +0 Dex, +2 Int; Init +1
- [ ] `Race_Gnome_ModifyIncomingDamage_PoisonResist` — Poison -15%
- [ ] `Race_Gnome_ModifyWeaponDamage_MeleeBoost` — Melee +15%
- [ ] `Race_StoneGiant_StatBonus` — +5 Str, -5 Dex, +0 Int; Init -3
- [ ] `Race_StoneGiant_ModifyIncomingDamage_PhysicResist` — Physic -15%

---

### ISpeciality / конкретные специализации

- [ ] `Speciality_NoSpeciality_SelectsFirst` — SelectTarget возвращает candidates.First()
- [ ] `Speciality_Warrior_SelectsHighestMaxHp` — SelectTarget выбирает с максимальным MaxHealth
- [ ] `Speciality_Ranger_SelectsLast` — SelectTarget возвращает candidates.Last()
- [ ] `Speciality_Assassin_SelectsLowestCurrentHp` — SelectTarget выбирает с минимальным CurrentHealth

---

### IPointsBudget / SharedPointsBudget

- [ ] `SharedPointsBudget_InitialPoints` — RemainingPoints == initialPoints
- [ ] `SharedPointsBudget_TrySpend_EnoughPoints_ReturnsTrue` — TrySpend(N) при N ≤ Remaining → true
- [ ] `SharedPointsBudget_TrySpend_NotEnough_ReturnsFalse` — TrySpend(N) при N > Remaining → false
- [ ] `SharedPointsBudget_TrySpend_DeductsPoints` — после TrySpend RemainingPoints уменьшается

---

### IItemCatalog<T> / WeaponCatalog, ArmorCatalog

- [ ] `WeaponCatalog_GetAll_ReturnsAllWeapons` — GetAll() возвращает 27 записей
- [ ] `ArmorCatalog_GetAll_ReturnsAllArmors` — GetAll() возвращает 6 записей
- [ ] `WeaponCatalog_GetAvailable_FiltersByPrice` — GetAvailable(maxPoints) возвращает только доступные
- [ ] `ArmorCatalog_GetAvailable_FiltersByPrice` — GetAvailable(maxPoints) возвращает только доступные
- [ ] `WeaponCatalog_GetByIndex_ReturnsCorrectEntry` — GetByIndex(0) == первый элемент из GetAll()
- [ ] `WeaponCatalog_GetByIndex_ThrowsOnInvalid` — неверный индекс → исключение

### CatalogEntry<T>

- [ ] `CatalogEntry_Create_SetsItemAndPrice` — Item и Price корректно проставлены

---

### IDamageService / DamageService

- [ ] `CalculateAttackDamage_ReturnsDamageStats` — корректные Min/MaxDamage с учётом стат, оружия, расы, брони
- [ ] `CalculateAttackDamage_CritApplied` — при NextDouble() ≥ CritChance крит применяется
- [ ] `CalculateAttackDamage_RaceModifierApplied` — модификаторы расы применяются к урону
- [ ] `CalculateAttackDamage_ArmorModifierApplied` — ModifyWeaponDamage брони применяется
- [ ] `CalculateReceivedDamage_ResistancesApplied` — сопротивления брони уменьшают урон
- [ ] `CalculateReceivedDamage_RandomRangeApplied` — урон в диапазоне [MinDamage, MaxDamage]
- [ ] `CalculateReceivedDamage_RaceIncomingModifierApplied` — ModifyIncomingDamage расы применяется

---

### IInitiativeService / InitiativeService

- [ ] `DetermineTurnOrder_ReturnsAllParticipants` — все участники присутствуют в результате
- [ ] `DetermineTurnOrder_SortedByInitiativeDesc` — отсортированы по инициативе (убывание)
- [ ] `DetermineTurnOrder_RaceModifierApplied` — модификатор инициативы от расы учитывается
- [ ] `DetermineTurnOrder_TiebreakerByDexterity` — при равной инициативе выше Dex идёт первым

### FighterInitiative

- [ ] `FighterInitiative_Create_SetsFighterAndScore` — Fighter и InitiativeScore корректны

---

### IBattleLogger / BattleLogger

- [ ] `LogMethods_DoNotThrow` — все Log-методы не выбрасывают исключений

---

### IRandomService / DefaultRandomService

- [ ] `DefaultRandomService_Next_ReturnsInRange` — Next(min, max) возвращает [min, max)
- [ ] `DefaultRandomService_NextDouble_ReturnsInRange` — NextDouble() возвращает [0.0, 1.0)

---

### GameManager

- [x] `Play_TwoEqualFighters_FirstFighterWins` — существующий тест
- [ ] `Play_OneFighterPerTeam_ReturnsSingleWinner` — возвращается 1 победитель при 1vs1
- [ ] `Play_FighterADies_FighterBWins` — если FighterA проигрывает, побеждает FighterB
- [ ] `Play_MultipleFighters_TeamWithLastAliveWins` — 2vs2, побеждает команда с выжившим
- [ ] `Play_AllDead_ReturnsEmpty` — если все умерли, возвращается пустой список
- [ ] `Play_RoundLogic_ExecutesUntilOneTeamRemains` — бой идёт пока не останется одна команда
- [ ] `Play_LoggerCalled_BattleStartAndEnd` — LogBattleStart и LogBattleEnd вызываются

---

### IFighterFactory / FighterFactory

- [ ] `FighterFactory_CreateFighter_ReturnsValidFighter` — созданный боец имеет все компоненты
- [ ] `FighterFactory_CreateFighterTeam_ReturnsTeamWithCorrectCount` — команда содержит указанное количество бойцов

### IFighterComponentFactory<T> реализации

- [ ] `RaceFactory_Create_ReturnsCorrectRace` — Create(1) → HumanRace, Create(2) → GoblinRace и т.д.
- [ ] `SpecialityFactory_Create_ReturnsCorrectSpeciality` — Create(1) → NoSpeciality, Create(2) → Warrior и т.д.

### IPointRestrictedFactory<T> реализации

- [ ] `WeaponFactory_TryCreate_EnoughPoints_CreatesAndDeducts` — TryCreate создаёт предмет и списывает очки
- [ ] `WeaponFactory_TryCreate_NotEnoughPoints_ReturnsFalse` — TryCreate возвращает false при нехватке очков
- [ ] `ArmorFactory_TryCreate_EnoughPoints_CreatesAndDeducts` — TryCreate создаёт броню и списывает очки
- [ ] `FighterStatFactory_TryCreate_CreatesStats` — TryCreate создаёт FighterStats по номеру выбора

---

### IFighterExtensions

- [ ] `IsAlive_HealthAboveZero_ReturnsTrue`
- [ ] `IsAlive_HealthZero_ReturnsFalse`

### DamageTypeExtensions

- [ ] `GetTypeNameRu_ReturnsRussianName` — каждый DamageType возвращает корректное русское название

---

### FightersApp.Run

- [ ] `Run_DoesNotThrow` — интеграционный тест, что точка входа не падает
