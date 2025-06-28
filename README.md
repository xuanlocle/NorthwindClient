# 📱 NorthwindClient

**NorthwindClient** is a cross-platform mobile application built with **.NET MAUI (.NET 9)**, designed to run on **Android and iOS**. It interacts with the Northwind API to manage customers and orders, and includes barcode scanning, state management, exception handling, and modern MVVM patterns.

---

## 🚀 Features

- 🧭 App Shell Navigation (multi-page)
- 📦 CRUD operations for Customers & Orders
- 🕹️ State management per screen: `Init`, `Success`, `Error`
- 📷 Barcode scanning (Android & iOS) using **ZXing.Net.MAUI**
- ❌ Global Exception Handling
- 📡 API service integration via `IApiService`
- 🔥 Firebase integration (WIP – Push Notifications)

---
# Project Assets

Here are the assets used in the project:

 <img alt="customer.png" src="assets/customer.png" width="300"/>
 <img alt="details.png" src="assets/details.png" width="300"/>
 <img alt="new_order.png"  src="assets/new_order.png" width="300"/>
 <img alt="scan_barcode.png"  src="assets/scan_barcode.png" width="300"/>

[![Video](file:assets/notification_deepink.mov)]
[![Video](file:assets/demo.mov)]

---

## 📦 Project Structure

~~~
/NorthwindClient
├── Models/ # OrderModel, CustomerModel, etc.
├── ViewModels/ # MVVM Toolkit ViewModels
├── Views/ # XAML pages
├── Services/ # Navigation & API abstraction
├── Infrastructure/ # Utility, constants, base classes
├── Resources/
│ ├── Fonts/
│ └── Images/
├── AppShell.xaml # MAUI Shell Navigation
├── MauiProgram.cs # Dependency Injection & startup
└── Platforms/ # Android & iOS configurations
~~~

## Try apk: 
[com.darrenle.northwind-Signed.apk](assets/com.darrenle.northwind-Signed.apk)

---

## 📷 Barcode Scanning

- Built with ZXing.Net.MAUI
- Opens popup on icon press
- Supports cancel button
- Validates barcode is numeric
- Auto-fills value into target entry

## 🔐 Configuration
- Base API URL securely stored in IAppConfig and injected via DI
- Consider using SecureStorage for auth/token if added in future

## ⚠️ Known TODOs
- Firebase push notifications
- User authentication & authorization
- Unit tests & UI test coverage

## 🤝 Contributing
Contributions are welcome! Open an issue for discussion or submit a PR directly.

## 📄 License
This project is open source and available under the MIT License.

Let me know if you'd like me to fill in Firebase setup, screenshots, or usage examples!