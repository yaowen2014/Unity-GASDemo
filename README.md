# Unity-GASDemo

这是一个基于 **Unity 实现的 Gameplay Ability System（GAS）** 示例工程，目标是在 Unity 中复刻并探索类似 Unreal Engine GAS 的能力系统设计思想。

项目重点展示了以下内容：

- GameplayAbility / GameplayEffect / Attribute 的基础设计
- Ability 授权（Grant）、激活、取消流程
- Attribute 修改与同步思路
- 以 Demo 场景形式验证 GAS 在实际玩法中的可行性

---

## 快速开始

### 克隆仓库

```bash
git clone https://github.com/yaowen2014/Unity-GASDemo.git
```

## 测试 

本项目主要用于测试和体验的场景是：

**`Assets/Demos/GAS_Tanks/Scenes/Tanks.unity`**

请务必打开该场景进行运行和测试。

该场景用于演示：

- AbilitySystem 的初始化流程
- 默认 Ability 的授予（Grant）
- Ability 的输入触发与取消
- Attribute 与 GameplayEffect 的实际效果

## 项目结构说明

```
Unity-GASDemo/
├─ Assets/
│  ├─ Demos/                    # Demo 文件夹
│  │  └─ GAS_Tanks/
│  │     └─ Scenes/
│  │        └─ Tanks.unity      # 核心测试场景
│  ├─ Scripts/                  # GAS 核心实现
│  └─ ...
├─ Packages/
│  ├─ manifest.json             # Unity 包依赖声明
│  └─ packages-lock.json        # 依赖锁定文件
├─ ProjectSettings/
└─ README.md
```