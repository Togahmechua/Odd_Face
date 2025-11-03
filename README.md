# 🎭 Odd Face

A fun and creative **memory & face-assembly puzzle game** now powered by **cloud technology**!  
Players drag and drop face parts loaded dynamically from remote server. Each part disappears after placement.  
When all parts are placed, the **full face is revealed**, and you can capture & save your masterpiece!  

---

## ⚡ **NEW: Remote Loading Technology**
- **🌐 Cloud-Powered Content**: Game now dynamically loads assets from remote server instead of bundling everything locally
- **📦 Smaller Build Size**: Reduced initial download size thanks to cloud integration
- **🔄 Live Updates**: New content can be added without requiring app store updates
- **📱 Multi-Platform Support**: Same cloud system works seamlessly on both Android and PC versions

---

## 📱 App Icon
<p align="left">
  <img width="200" height="200" alt="app icon" src="https://github.com/user-attachments/assets/5253c13c-ad96-47d1-abe1-19b85ed7ea45" />
</p>

---

## 🖼️ Screenshots
| | | |
|---|---|---|
| <img width="320" src="https://github.com/user-attachments/assets/dc9be249-f249-4164-a4fa-f53a650bd1c7" /> | <img width="320" src="https://github.com/user-attachments/assets/9e45e454-bfa6-468f-9251-766980536325" /> | <img width="320" src="https://github.com/user-attachments/assets/99e241f6-aee4-4e39-893b-54f7594ef0f8" /> |
| <img width="320" src="https://github.com/user-attachments/assets/dd9370d6-d19a-462a-ac34-e9bd50c10efb" /> | <img width="320" src="https://github.com/user-attachments/assets/fdf94ff4-2011-4214-aeec-29f13e8f1684" /> | <img width="320" src="https://github.com/user-attachments/assets/3405f5fb-9b76-4d91-83ab-525e9fb24727" /> |
| <img width="320" src="https://github.com/user-attachments/assets/72a887e7-a635-46cd-8a84-fd4863e26e7e" /> | <img width="320" src="https://github.com/user-attachments/assets/b1eea341-897d-443b-968a-bb3a51ad2f4d" /> | <img width="320" src="https://github.com/user-attachments/assets/d511f52d-8e80-4e5f-8269-cc35c691b1f8" /> |
| <img width="320" src="https://github.com/user-attachments/assets/11c0684f-74d9-4994-a4f1-713753efe926" /> | <img width="320" src="https://github.com/user-attachments/assets/2f5b7b4f-8373-4a03-8f32-36c56f779597" /> | <img width="320" src="https://github.com/user-attachments/assets/a7298e30-b329-48e9-ab8f-5a011c8f0bf7" /> |

---

## 🚀 Features
- 🎨 **Drag & Drop Gameplay** – pick, place, and hide parts as you build the face  
- 🧠 **Memory Challenge** – parts vanish after placing, only revealed at the end  
- 📸 **Screenshot Mode** – capture your final face with one click  
- 💡 **Minimal & Fun** – designed for short, joyful play sessions  
- 📂 **Save & Share** – automatically saves to gallery, making it easy to share with friends  
- 🎭 **Replayability** – every attempt creates a new funny face combination  
- 🌐 **Remote Loading** – assets loaded dynamically from cloud server

---

## 🎮 How to Play
1. **Start** the game and wait for assets to load from server
2. **Drag & Drop** the part into the correct position  
3. The part will **disappear after placement** – challenging your memory!  
4. Repeat until **all parts are placed**  
5. The **full face is revealed** 🎉  
6. Use the **screenshot button** to save your creation to the gallery  

---

## 📂 Save & Share
The game comes with a built-in **screenshot capture system**:
- When the player completes a face, they can **take a snapshot** of their creation  
- The screenshot is saved directly to the **device's gallery/storage**  
- Players can **share their results** on social media or messaging apps with ease  
- Works on both **real devices** and **emulators** (for testing)  

---

## ⚙️ Tech Stack
- **Unity 2022.3.24 LTS** – stable and lightweight for 2D casual games  
- **C#** – handles game logic, drag & drop interactions, and face assembly  
- **Custom Screenshot Capture** – allows saving and sharing player creations  
- **Mobile & Emulator Friendly** – tested on both devices and emulators
- **Unity Addressables** – advanced asset management system for remote loading
- **Netlify CDN** – high-performance cloud hosting for game assets
- **Cross-Platform Architecture** – single codebase supporting Android, PC, and WebGL

---

## 🛠️ Architecture & Code Practices
- **ScriptableObjects** – used to store part data (face elements, configs) in a clean and reusable way  
- **SOLID Principles** – code is structured into small, maintainable classes that follow single-responsibility and interface segregation  
- **Interfaces** – ensure flexibility and scalability when adding new part types or gameplay mechanics  
- **Object Pooling** – optimizes performance by reusing face part objects instead of creating/destroying repeatedly  
- **Event-driven Design** – UI updates and gameplay logic are loosely coupled, improving testability and maintainability
- **Remote Asset Loading** – implements Unity Addressables for dynamic content delivery
- **Platform Detection** – automatic server path resolution for different platforms
- **Cache Management** – efficient asset caching with proper validation

---

## 🌐 Play Now
👉 Try it here: [Odd Face on itch.io](https://togahmechua.itch.io/odd-face)  

**Now with cloud-powered content delivery!** 🚀
