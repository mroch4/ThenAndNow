# Poznań - Dawniej&Dziś | Poznań - Then&Now

## Url

https://poznandawniejdzis.pl

https://poznan-dawniej-dzis.pl

## Used libraries

1. [Bootstrap][def]
2. [Leaflet][def2]

## Todos

1. Auth user using Firebase
2. \*GitHub Security alert: Firebase API key (?)

## Release history

1. Q4_2020/Q1_2021 - **static HTML** with minimal JS (hardcoded 10 entries)

2. Q4_2021 - **Angular**(1):

   - dynamic data load from JSON to component templates
   - voting using Firebase Realtime Database
   - images depending on screen resolution (three tiers)
   - URL: http://poznandawniejdzis.pl/
   - [LinkedIn][def5]

3. Q4_2022 - **Angular**(2):

   - UX/UI redesign
   - improved responsiveness
   - _.jpg > _.webp
   - URL: https://poznan-dawniej-dzis.pl/#/

4. Q2_2023 - **React + Typescript**, not released due to:

   - lack of state animations
   - missing dependency injection

5. Q4_2024 - **Blazor WASM + Azure Cosmos DB**, not released due to:

   - no ability to locally establish connection to Azure database
   - not fully integrated [Leaflet][def4] library for Blazor (non-clickable markers)
   - lack of state animations

6. Q1_2025 - **Angular**(3):

   - Angular update 12.2.0 > 19.1.0
   - routing using query params
   - HTML content as labels (language packs for the future)
   - added share buttons
   - added DTOs
   - load entry details (description) on demand from Firebase Realtime Database - for 30 entries lowered entires.json size from 26,343 to 7,191 bytes (-73%)
   - URL: http://www.poznandawniejdzis.pl

7. Q4_2025 - **Blazor WASM + Firebase Realtime Database**
   - URL: https://poznan-dawniej-dzis.pl/

## Photos timeline

1. 2016/06/07 - entries: 4, 6, 8, 25, 26
2. 2016/06/08 - entries: 1, 2, 3
3. 2019/01/19 - entries: 5, 7, 9, 10, 11
4. 2021/10/02 - entries: 12, 13, 14, 15, 16, 17
5. 2021/10/30 - entries: 18, 19, 20
6. 2022/04/23 - entries: 21, 22, 27, 30
7. 2022/12/04 - entries: 23, 24
8. 2022/12/17 - entries: 28, 29
9. 2023/08/15 - entries: 31, 32, 33
10. 2024/05/01 - entries: 34, 35, 36, 37, 38, 39
11. 2025/03/06 - entry: 40

## Photos dimensions

1. SM - 720px; max screen width: 576px; ratio: 720/576 = 1.25
2. MD - 1200px; max screen width: 992px; ratio: 1200/992 = 1.21
3. LG - 1600px

[def]: https://getbootstrap.com/
[def2]: https://github.com/Leaflet/Leaflet
[def4]: https://github.com/ichim/LeafletForBlazor-NuGet
[def5]: https://www.linkedin.com/posts/marcin-rochowski-68443921b_pozna%C5%84-dawniej-dzi%C5%9B-activity-6862803538507030528-0EOT?utm_source=share&utm_medium=member_desktop
