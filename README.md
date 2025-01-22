# Grid Number Challenge

A fast-paced memory game where players must click numbers in sequence while the grid continuously shuffles. Test your reflexes and memory as you race against time to complete the sequence!

## 🎮 Gameplay

Players must:
1. Click numbers in sequential order (1, 2, 3...)
2. Keep track of numbers as they shuffle across the grid
3. Complete the sequence before time runs out
4. Avoid wrong clicks which result in time penalties

## 🎯 Features

- Dynamic grid shuffling with smooth animations
- Adjustable game settings:
  - Shuffle interval (0.5s - 5s)
  - Time penalty percentage (0% - 50%)
  - Game duration (20s - 120s)
- Visual feedback for correct and incorrect clicks
- Progress bar showing remaining time
- Settings panel for real-time game customization

## 🛠️ Technical Details

### Built With
- Unity 2021.3+
- C#
- TextMeshPro

### Key Scripts

1. **GameManager.cs**
   - Controls core game logic
   - Handles grid creation and shuffling
   - Manages game state and timing

2. **Cell.cs**
   - Individual cell behavior
   - Handles click events
   - Manages visual states

3. **GameSettings.cs**
   - Controls settings panel
   - Manages game parameters
   - Real-time game adjustments

### Prerequisites
- Unity 2021.3 or higher
- TextMeshPro package

## 📥 Installation

1. Clone the repository
```bash
git clone https://github.com/Kaustubh0912/Grid_Challlenge
```

2. Open the project in Unity

3. Ensure TextMeshPro package is imported

4. Open the main scene in `Assets/Scenes/GameScene`

## 🎨 Customization

### Visual Styling
- Modify cell colors in the Cell prefab
- Adjust UI elements in Canvas
- Edit transition animations in the Animation folder

### Game Parameters
Default values can be adjusted in the Inspector:
- Grid Size: 20 cells (5x4)
- Game Time: 60 seconds
- Shuffle Interval: 2 seconds
- Penalty: 5% of remaining time

## 🎓 How to Play

1. Click the "Start Game" button
2. Find and click number 1
3. Continue clicking numbers in sequence
4. Watch for shuffling grid positions
5. Complete the sequence before time runs out

### Tips
- Keep track of multiple numbers ahead
- Learn the shuffle patterns
- Use the settings to find your optimal challenge level

## 🔧 Settings Panel

Access game settings through the side panel:

| Setting | Range | Description |
|---------|-------|-------------|
| Shuffle Interval | 0.5s - 5s | Time between grid shuffles |
| Penalty | 0% - 20% | Time deduction for wrong clicks |
| Game Time | 30s - 120s | Total time to complete sequence |


## 🐛 Known Issues

1. Grid might flicker during first shuffle
2. Settings panel may need double-click in some cases
3. Extreme shuffle speeds might affect performance

## 🚀 Future Enhancements

- [ ] Multiple difficulty modes
- [ ] High score system
- [ ] Sound effects and background music
- [ ] Power-ups and special effects
- [ ] Mobile touch support
- [ ] Online leaderboards
- [ ] Different grid patterns
- [ ] Achievement system

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## 🙋‍♂️ Support

For support, email [kaustubharun2003@gmail,com](mailto:kaustubharun2003@gmail,com) or open an issue in this repository.

## 🤝 Contributing

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👏 Acknowledgments

- Inspired by memory and reaction games
- UI design inspired by modern minimal interfaces
- Special thanks to the Unity community


---

Made with ❤️ by [KCozy]

[Unity Version: 2022.3.44f1]
