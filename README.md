# SimpleBookStore

這是一個簡易的線上書店專案，提供使用者瀏覽書籍、註冊帳號、購物車結帳等基本功能。專案以 C# 為主要後端語言，並使用 ASP.NET Core MVC 框架實作。

## 線上體驗

👉 [Demo 部署網站](https://simplebookstore.hollowtim.dev/)

> 本專案部署於 Google Cloud Run，首次開啟可能需等待服務啟動，請耐心等候。

- 建議使用測試帳號登入，或自行註冊（需信箱驗證）。

## 功能特色

- Entity Framework 搭配 MySQL
- 使用 Identity 實作註冊、登入、忘記密碼、密碼重設（信箱驗證透過 Brevo API）
- 書籍資訊瀏覽、搜尋
- 購物車、結帳、訂單歷史查詢
- 管理員後台（訂單、商品、分類、作者、優惠卷、會員管理）

## 測試帳號

| 帳號                | 密碼      |
| ------------------- | --------- |
| test01@12345.com    | Aa12345   |

> 亦可直接使用「註冊」功能建立新帳號體驗（需信箱驗證）。

## 專案架構簡介

- **後端語言**：C#
- **前端技術**：jQuery, Bootstrap 5
- **資料庫**：MySQL
- **框架**：ASP.NET Core MVC
- **ORM**：Entity Framework Core（Code First）

---

本專案為個人作品集，著重展示功能與技術實作。如有建議歡迎提 Issue 或聯絡 [hollowtimTW](https://github.com/hollowtimTW)。
