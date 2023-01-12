# Вакансия :: C#-программист

Разработка бизнес-системы с использованием веб-технологий. Написание native desktop приложений для Windows.

## От вас

### Обязательно

- Знание синтаксиса языка C#. Опыт разработки под .NET Core и .NET Framework 5+
- Базовые знания принципов работы Web
- Работа с WPF/WinForms/UWP
- Желание работать в команде и развиваться

### Приветствуется

- Навыки работы с ASP.NET MVC, .NET Core
- Знание баз данных: PostgreSQL, MS SQL
- Опыт разработки под ОС семейства GNU Linux
- Работа с Git
- Знания базовых принципов разработки (тестирование, рефакторинг, Code Review)
- Опыт переноса приложений со старых версий .NET на .Net Core/.Net 5+
- Опыт работы с Docker. Знание Dockerfile и docker-compose. Развертывание приложений в Docker

### Будет круто, но не обязательно

- Знание английского языка на уровне чтения технической документации
- Опыт разработки на других современных языках программирования
- Участие в  Open Source проектах
- Наличие профиля на GitHub, Stack Overflow
- Наличие проектов которые можете продемонстрировать

## У нас

- Полный рабочий день, гибкий обед по желанию сотрудника, гибкое время начала рабочего дня
- Полностью «белая» заработная плата с возможностью увеличения в процессе работы (зависит от отдачи сотрудника)
- Добровольное медицинское страхование
- Дружелюбная команда с юмором, готовая поддержать
- Интересный проект и необычные задачи. Нет, если думаете, что рутины нет, она есть, но нацелены мы именно на продуктив
- Возможность одновременно участвовать в разных проектах и развивать другие компетенции (последний .NET 5 и все модное)
- Возможность попробовать современные тренды и практики в разработке ПО
- По желанию: один день в неделю - удаленная работа
- Никаких опенспейсов, а комфортное пространство в центре Тюмени
- Готовы безгранично делиться опытом при условии, что вы готовы принимать

Если у вас есть опыт работы с 1С, то эта вакансия не для вас. Даже не пытайтесь.
Если вакансия вас заинтересовала, но есть какие-то недопонимания и вопросы, приходите, обсудим, договоримся.  
Большим плюсом будет решение тестового задания. Решение принимается в виде PR к текущему проекту.

## Тестовое задание

Есть несколько рабочих сервисов, у каждого сервиса есть состояние работает/не работает/работает нестабильно.

Требуется написать API который:

1. Получает и сохраняет данные: имя, состояние, описание
2. Выводит список сервисов с актуальным состоянием
3. По имени сервиса выдает историю изменения состояния и все данные по каждому состоянию

Дополнительным плюсом будет

1. По указанному интервалу выдается информация о том сколько не работал сервис и считать SLA в процентах до 3-й запятой

Вывод всех данных должен быть в формате JSON
