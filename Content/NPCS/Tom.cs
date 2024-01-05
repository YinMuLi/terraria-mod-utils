using Branch.Common.Configs;
using Branch.Content.Items;
using Branch.Content.Items.Potions;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Branch.Content.NPCS
{
    /// <summary>
    /// 模组NPC-Tom
    /// </summary>
    [AutoloadHead] //一小段代码就是自动加载一个NPC的小地图图标用的
    internal class Tom : ModNPC
    {
        /// <summary>
        /// 正在显示的商店编号
        /// 0：渔夫商店
        /// 1：百草园
        /// </summary>
        public static int shopNum = 0;

        /// <summary>
        /// 商店的数量
        /// </summary>
        public const int SHOP_COUNT = 3;

        private const string UI_PREFIX = "Mods.UI.Tom";

        /// <summary>
        /// 渔夫商店
        /// </summary>
        private string anglerShop => Language.GetTextValue($"{UI_PREFIX}.AnglerShop");

        /// <summary>
        /// 百草园
        /// </summary>
        private string herbsShop => Language.GetTextValue($"{UI_PREFIX}.HerbsShop");

        /// <summary>
        /// 通用商店
        /// </summary>
        private string generalShop => Language.GetTextValue($"{UI_PREFIX}.GeneralShop");

        #region 基础设置

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.SpawnTom;
        }

        public override void SetStaticDefaults()
        {
            //总帧数，根据使用贴图的实际帧数进行填写，这里我们直接调用全部商人的数据
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Merchant];
            //特殊交互帧（如坐下，攻击）的数量，其作用就是规划这个NPC的最大行走帧数为多少，
            //最大行走帧数即Main.npcFrameCount - NPCID.Sets.ExtraFramesCount
            NPCID.Sets.ExtraFramesCount[Type] = 9;
            //攻击帧的数量，取决于你的NPC属于哪种攻击类型，如何填写见上文的分类讲解
            NPCID.Sets.AttackFrameCount[Type] = 4;
            //NPC的攻击方式，同样取决于你的NPC属于哪种攻击类型，投掷型填0，远程型填1，魔法型填2，近战型填3，
            //如果是宠物没有攻击手段那么这条将不产生影响
            NPCID.Sets.AttackType[Type] = 0;
            //NPC的帽子位置中Y坐标的偏移量，这里特指派对帽，
            //当你觉得帽子戴的太高或太低时使用这个做调整（所以为什么不给个X的）
            NPCID.Sets.HatOffsetY[Type] = 2;
            //NPC的单次攻击持续时间，如果你的NPC需要持续施法进行攻击可以把这里设置的很长，
            //比如树妖的这个值就高达600
            //补充说明一点：如果你的NPC的AttackType为3即近战型，
            //这里最好选择套用，因为近战型NPC的单次攻击时间是固定的
            NPCID.Sets.AttackTime[Type] = 90;
            //NPC在遭遇敌人时发动攻击的概率，如果为0则该NPC不会进行攻击（待验证）
            //遇到危险时，该NPC在可以进攻的情况下每帧有 1 / (NPCID.Sets.AttackAverageChance * 2) 的概率发动攻击
            //注：每帧都判定
            NPCID.Sets.AttackAverageChance[Type] = 30;
            //NPC的危险检测范围，以像素为单位，这个似乎是半径
            NPCID.Sets.DangerDetectRange[Type] = 500;
            //NPC具有闪烁纹理
            NPCID.Sets.ShimmerTownTransform[Type] = true;
            //图鉴设置部分
            //将该NPC划定为城镇NPC分类
            NPCID.Sets.TownNPCBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                //为NPC设置图鉴展示状态，赋予其Velocity即可展现出行走姿态
                Velocity = 1f,
                Direction = -1
            };
            //添加信息至图鉴
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            //设置对邻居和环境的喜恶，也就是幸福度设置
            //幸福度相关对话需要写在hjson里
            NPC.Happiness
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Hate)//憎恶丛林环境
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)//讨厌地下环境
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like)//喜欢雪地环境
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)//最爱海洋环境
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Dislike)//讨厌与渔夫做邻居
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)//喜欢与向导做邻居
                                                                  //邻居的喜好级别和环境的AffectionLevel是一样的
            ;
        }

        public override void SetDefaults()
        {
            //判断该NPC是否为城镇NPC，决定了这个NPC是否拥有幸福度对话，是否可以对话以及是否会被地图保存
            //当然以上这些属性也可以靠其他的方式开启或关闭
            NPC.townNPC = true;
            //该NPC为友好NPC，不会被友方弹幕伤害且会被敌对NPC伤害
            NPC.friendly = true;
            //碰撞箱宽，不做过多解释，此处为标准城镇NPC数据
            NPC.width = 18;
            //碰撞箱高，不做过多解释，此处为标准城镇NPC数据
            NPC.height = 40;
            //套用原版城镇NPC的AIStyle，这样我们就不用自己费劲写AI了，
            //同时根据我以往的观测结果发现这个属性也决定了NPC是否会出现在入住列表里
            NPC.aiStyle = NPCAIStyleID.Passive;
            //伤害，由于城镇NPC没有体术所以这里特指弹幕伤害（虽然弹幕伤害也是单独设置的所以理论上这个可以不写？）
            NPC.damage = 10;
            NPC.defense = 40;
            NPC.lifeMax = 260;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            //抗击退性，数据越小抗性越高
            NPC.knockBackResist = 0.5f;
            //模仿的动画类型，这样就不用自己费劲写动画播放了
            AnimationType = NPCID.Merchant;
            //NPC是否可以被虫网类似的工具捕获
            Main.npcCatchable[NPC.type] = false;
            //当被捕获时，该物品的ID，捕虫网只会对ID>0的NPC有效。
            NPC.catchItem = -1;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            //设置图鉴信息
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                //设置所属环境，一般填写他最喜爱的环境
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                //图鉴内描述
                new FlavorTextBestiaryInfoElement("Tom这个名字你能想到什么？")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            //返回条件为：拥有两个NPC
            return numTownNPCs >= 2 && ServerConfig.Instance.SpawnTom;
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            //toKingStatue 也就是可以被国王雕像传送，这样一来测试小伙就是个男生了；
            //如果你想让她是个女生则可以返回!toKingStatue，也就是不能被国王雕像传送、但可以被王后雕像传送。
            //如果你想要它无性别，那就直接返回false，双性别（？？）则可以直接返回true。
            return !toKingStatue;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            //Profile的功能就是控制你的NPC被分配到的贴图以及小地图头像还有姓名。
            //普通城镇NPC并不需要太复杂的Profile，如果你的NPC不需要换材质甚至可以不需要Profile
            //但城镇宠物这种拥有随机外观的会很需要用到。
            return new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture));
        }

        #endregion 基础设置

        #region 对话与商店

        public override string GetChat()
        {
            string prefix = "Mods.NPCs.Tom.Chat";
            //设置对话
            WeightedRandom<string> chat = new();
            //无家可归时,必须if和else都设置，不然NPC有家时没有对话，导致对话框
            //显示不出来
            if (NPC.homeless)
            {
                chat.Add(Language.GetTextValue($"{prefix}1"));
            }
            else
            {
                chat.Add(Language.GetTextValue($"{prefix}3"));
            }
            //正在举行派对时
            if (BirthdayParty.PartyIsUp)
            {
                chat.Add(Language.GetTextValue($"{prefix}2"));
            }
            return chat;
        }

        /// <summary>
        /// 获取商店名称
        /// </summary>
        /// <returns>string</returns>
        private string GetShopName()
        {
            return shopNum switch
            {
                0 => anglerShop,
                1 => herbsShop,
                2 => generalShop,
                _ => null
            };
        }

        //设置对话按钮的文本
        public override void SetChatButtons(ref string button, ref string button2)
        {
            //打开NPC对话框，每帧刷新
            button = GetShopName();
            //设置第二个按钮，实际上是第三个按钮
            button2 = Language.GetTextValue($"{UI_PREFIX}.ChangeButton");
        }

        //设置当对话按钮被摁下时会发生什么
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            //当第一个按钮被按下时
            if (firstButton)
            {
                shopName = GetShopName();
            }
            //如果是第二个按钮被按下时
            else
            {
                shopNum = (shopNum + 1) % SHOP_COUNT;
            }
        }

        public override void AddShops()
        {
            NPCShop shop = null;
            short[] items = null;

            #region 渔夫商店

            shop = new NPCShop(Type, anglerShop);
            items = new short[]
            {
            ItemID.SonarPotion,//声呐药水
            ItemID.FishingPotion,//钓鱼药水
            ItemID.CratePotion,//宝匣药水
            ItemID.ApprenticeBait,//学徒鱼饵
            //渔夫一套
            ItemID.AnglerHat,
            ItemID.AnglerVest,
            ItemID.AnglerPants,
            ItemID.GoldenFishingRod,
            ItemID.GoldenBugNet,
            ItemID.AnglerEarring,
            ItemID.TackleBox,
            ItemID.FishermansGuide,
            ItemID.WeatherRadio,
            ItemID.Sextant,
            //防熔岩钓钩
            ItemID.LavaFishingHook,
            //优质钓鱼线
            ItemID.HighTestFishingLine,
            ItemID.SuperAbsorbantSponge,
            ItemID.BottomlessBucket,
            };
            for (int i = 0; i < items.Length; i++)
            {
                shop.Add(new Item(items[i]));
            }
            shop.Register();

            #endregion 渔夫商店

            #region 药房

            shop = new NPCShop(Type, herbsShop);

            items = new short[]
            {
                ItemID.Blinkroot,//闪耀根
                ItemID.Daybloom,//太阳花
                ItemID.Deathweed,//死亡草
                ItemID.Fireblossom,//火焰花
                ItemID.Moonglow,//月光草
                ItemID.Shiverthorn,//寒颤棘
                ItemID.Waterleaf,//幌菊
                ItemID.BottledWater//瓶装水
            };
            for (int i = 0; i < items.Length; i++)
            {
                shop.Add(new Item(items[i]));
            }
            shop.Register();

            #endregion 药房

            #region 通用商店

            shop = new NPCShop(Type, generalShop);
            shop.Add(new Item(ItemID.TorchGodsFavor));
            shop.Add(new Item(ItemID.TeleportationPylonVictory));//万能晶塔
            shop.Add<AdvancedDefensePotion>(Condition.Hardmode);
            shop.Add(new Item(ItemID.Bacon), Condition.Hardmode);//培根
            shop.Add(new Item(ItemID.TruffleWorm), Condition.Hardmode);//松露虫
            shop.Add<FinalStation>(Condition.Hardmode);
            shop.Add(new Item(ItemID.TerrasparkBoots), Condition.DownedSkeletron);//泰拉闪耀靴
            shop.Add<AdvancedExplorerPotion>(Condition.DownedSkeletron);
            shop.Add(new Item(ItemID.Shellphone), Condition.DownedSkeletron);//贝壳电话
            shop.Add(new Item(ItemID.Meteorite), Condition.DownedEowOrBoc);//陨石
            shop.Add(new Item(ItemID.Autohammer), Condition.DownedPlantera);//自动锤炼机（世纪之花）
            shop.Add(new Item(ItemID.ChlorophyteBar), Condition.DownedPlantera);//叶绿锭
            shop.Add(new Item(ItemID.EmpressButterfly), Condition.DownedPlantera); //七彩草蛉
            shop.Register();

            #endregion 通用商店
        }

        #endregion 对话与商店

        #region 攻击与防护

        //设置NPC的攻击力
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            //伤害，直接调用NPC本体的伤害
            damage = NPC.damage;
            //击退力，中规中矩的数据
            knockback = 3f;
        }

        //设置每次攻击完毕后的冷却时间
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            //基础冷却时间
            cooldown = 5;
            //额外冷却时间
            randExtraCooldown = 30;
        }

        //设置发射的弹幕
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            //射弹种类
            projType = ProjectileID.FlamingJack;
            //弹幕发射延迟，最好只给魔法型NPC设置较高数据
            attackDelay = 10;
        }

        //设置发射弹幕的向量
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            //发射速度
            multiplier = 6f;
            //射击角度额外向上偏移的量
            //gravityCorrection = 0f;
            //射击时产生的最大额外向量偏差
            //randomOffset = 0.5f;
        }

        #endregion 攻击与防护
    }
}