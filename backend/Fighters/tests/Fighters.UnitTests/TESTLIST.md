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

### IArmor / конкретные брони

- [x] `Armor_NoArmor_ResistancesEmpty` — Resistances пустой; ModifyWeaponDamage/ModifyIncomingDamage возвращают как есть
- [x] `Armor_LeatherArmor_Resistances_Physic10` — Resistances содержит Physic=0.1f
- [x] `Armor_KnightArmor_Resistances` — Physic=0.4, Poison=0.1, Magic=0.05
- [x] `Armor_GlassArmor_DoublesDamage` — ModifyWeaponDamage и ModifyIncomingDamage удваивают урон
- [x] `Armor_BerserkArmor_ScalesWithMissingHp` — ModifyWeaponDamage множитель растёт при потере HP
- [x] `Armor_WitchDoctorArmor_ModifyIncomingDamage` — Physic/Pure → Magic, -20% dmg, -15% crit
- [x] `Armor_WitchDoctorArmor_ModifyWeaponDamage` — Magic/Poison +20%, Physic/Pure → Magic +15%

---

### IRace — уникальные бонусы рас

- [x] `Race_Goblin_ModifyWeaponDamage_RarityScaling` — корректирует урон в зависимости от Rarity (Common предметы → -10)
- [x] `Race_Goblin_ModifyWeaponDamage_LegendaryGear_BoostsDamage` — Legendary предметы (MonoBlade + BerserkArmor) → +30 к урону
- [x] `Race_Drow_ModifyWeaponDamage_RangedBonus` — +50 damage у Ranged оружия
- [x] `Race_Gnome_ModifyIncomingDamage_PoisonResist` — Poison -15%
- [x] `Race_Gnome_ModifyWeaponDamage_MeleeBoost` — Melee +15%
- [x] `Race_StoneGiant_ModifyIncomingDamage_PhysicResist` — Physic -15%

---

### ISpeciality / конкретные специализации

- [x] `Speciality_NoSpeciality_SelectsFirst` — SelectTarget возвращает candidates.First()
- [x] `Speciality_Warrior_SelectsHighestMaxHp` — SelectTarget выбирает с максимальным MaxHealth
- [x] `Speciality_Ranger_SelectsLast` — SelectTarget возвращает candidates.Last()
- [x] `Speciality_Assassin_SelectsLowestCurrentHp` — SelectTarget выбирает с минимальным CurrentHealth

---

### IPointsBudget / SharedPointsBudget

- [x] `SharedPointsBudget_InitialPoints` — RemainingPoints == initialPoints
- [x] `SharedPointsBudget_TrySpend_EnoughPoints_ReturnsTrue` — TrySpend(N) при N ≤ Remaining → true
- [x] `SharedPointsBudget_TrySpend_NotEnough_ReturnsFalse` — TrySpend(N) при N > Remaining → false
- [x] `SharedPointsBudget_TrySpend_DeductsPoints` — после TrySpend RemainingPoints уменьшается

---

### IItemCatalog<T> (через TestCatalog с моковыми данными)

- [x] `GetAll_ReturnsAllEntries` — возвращает все записи
- [x] `GetAvailable_FiltersByPrice` — фильтрация по maxPoints
- [x] `GetAvailable_ZeroPoints_ReturnsFreeItems` — только бесплатные предметы при 0 очков
- [x] `GetByIndex_ReturnsCorrectEntry` — получение по индексу
- [x] `GetByIndex_ThrowsOnInvalidIndex` — исключение при неверном индексе

### CatalogEntry<T>

- [x] `CatalogEntry_Create_SetsItemAndPrice` — Item и Price корректно проставлены

---

### IDamageService / DamageService

- [x] `CalculateAttackDamage_ReturnsDamageStats` — корректные Min/MaxDamage с учётом стат, оружия, расы, брони
- [x] `CalculateAttackDamage_CritApplied` — при NextDouble() ≥ CritChance крит применяется
- [x] `CalculateAttackDamage_RaceModifierApplied` — модификаторы расы применяются к урону
- [x] `CalculateAttackDamage_ArmorModifierApplied` — ModifyWeaponDamage брони применяется
- [x] `CalculateReceivedDamage_ResistancesApplied` — сопротивления брони уменьшают урон
- [x] `CalculateReceivedDamage_RandomRangeApplied` — урон в диапазоне [MinDamage, MaxDamage]
- [x] `CalculateReceivedDamage_RaceIncomingModifierApplied` — ModifyIncomingDamage расы применяется

---

### IInitiativeService / InitiativeService

- [x] `DetermineTurnOrder_SortedByInitiativeDesc` — отсортированы по инициативе (убывание)
- [x] `DetermineTurnOrder_RaceModifierApplied` — модификатор инициативы от расы учитывается
- [x] `DetermineTurnOrder_TiebreakerByDexterity` — при равной инициативе выше Dex идёт первым

---

### IBattleLogger / BattleLogger (через GameManager)

- [x] `GameManager_CallsLogBattleStartAndEnd_AtLeastOnce` — мок IBattleLogger; GameManager вызывает LogBattleStart, LogBattleEnd, LogRoundStart, LogRoundEnd, LogAttack, LogDamageTaken хотя бы 1 раз

---

### IRandomService / DefaultRandomService

- [x] `DefaultRandomService_Next_ReturnsInRange` — Next(min, max) возвращает [min, max)
- [x] `DefaultRandomService_NextDouble_ReturnsInRange` — NextDouble() возвращает [0.0, 1.0)

---

### GameManager

- [x] `Play_TwoEqualFighters_FirstFighterWins` — существующий тест
- [x] `Play_OneFighterPerTeam_ReturnsSingleWinner` — возвращается 1 победитель при 1vs1
- [x] `Play_FighterADies_FighterBWins` — если FighterA проигрывает, побеждает FighterB
- [x] `Play_MultipleFighters_TeamWithLastAliveWins` — 2vs2, побеждает команда с выжившим
- [x] `Play_RoundLogic_ExecutesUntilOneTeamRemains` — бой идёт пока не останется одна команда
- [x] `Play_LoggerCalled_BattleStartAndEnd` — LogBattleStart и LogBattleEnd вызываются

---

### IFighterFactory / FighterFactory

- [x] `FighterFactory_CreateFighter_ReturnsValidFighter` — созданный боец имеет все компоненты
- [x] `FighterFactory_CreateFighterTeam_ReturnsTeamWithCorrectCount` — команда содержит указанное количество бойцов

### IFighterComponentFactory<T> реализации

- [x] `RaceFactory_Create_ReturnsCorrectRace` — Create(0) → HumanRace, Create(1) → DrowRace и т.д.
- [x] `SpecialityFactory_Create_ReturnsCorrectSpeciality` — Create(0) → NoSpeciality, Create(1) → Warrior и т.д.

### IPointRestrictedFactory<T> реализации

- [x] `WeaponFactory_TryCreate_EnoughPoints_CreatesAndDeducts` — TryCreate создаёт предмет и списывает очки
- [x] `WeaponFactory_TryCreate_NotEnoughPoints_ReturnsFalse` — TryCreate возвращает false при нехватке очков
- [x] `ArmorFactory_TryCreate_EnoughPoints_CreatesAndDeducts` — TryCreate создаёт броню и списывает очки
- [x] `FighterStatFactory_TryCreate_CreatesStats` — TryCreate создаёт FighterStats по номеру выбора

---

### IFighterExtensions

- [x] `IsAlive_HealthAboveZero_ReturnsTrue`
- [x] `IsAlive_HealthZero_ReturnsFalse`

### DamageTypeExtensions

- [x] `GetTypeNameRu_ReturnsRussianName` — каждый DamageType возвращает корректное русское название

---

### FightersApp.Run

- [x] `Run_DoesNotThrow` — интеграционный тест, что точка входа не падает
