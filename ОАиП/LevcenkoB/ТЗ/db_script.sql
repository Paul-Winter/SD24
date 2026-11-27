-- Создание базы данных и подключение к ней
CREATE DATABASE schedule_db;
\c schedule_db;

-- Удаление старых таблиц, если они были (чтобы скрипт можно было запускать повторно)
DROP TABLE IF EXISTS grades CASCADE;
DROP TABLE IF EXISTS schedule CASCADE;
DROP TABLE IF EXISTS absences CASCADE;
DROP TABLE IF EXISTS subjects CASCADE;
DROP TABLE IF EXISTS teachers CASCADE;
DROP TABLE IF EXISTS groups CASCADE;

-- 1. Таблица групп
CREATE TABLE groups (
    id SERIAL PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    student_count INT NOT NULL,
    course INT NOT NULL
);

-- 2. Таблица преподавателей (со всеми новыми полями)
CREATE TABLE teachers (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    birth_year INT,
    experience_years INT,
    specialization TEXT
);

-- 3. Таблица предметов
CREATE TABLE subjects (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

-- 4. Таблица отсутствий
CREATE TABLE absences (
    id SERIAL PRIMARY KEY,
    teacher_id INT REFERENCES teachers(id) ON DELETE CASCADE,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    type VARCHAR(20) NOT NULL CHECK (type IN ('отпуск', 'больничный', 'выходной')),
    reason TEXT
);

-- 5. Таблица расписания
CREATE TABLE schedule (
    id SERIAL PRIMARY KEY,
    teacher_id INT REFERENCES teachers(id) ON DELETE CASCADE,
    subject_id INT REFERENCES subjects(id) ON DELETE CASCADE,
    group_id INT REFERENCES groups(id) ON DELETE CASCADE,
    auditorium_number VARCHAR(20),
    day_of_week INT NOT NULL CHECK (day_of_week BETWEEN 1 AND 7),
    lesson_date DATE, 
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    status VARCHAR(20) DEFAULT 'ожидается' CHECK (status IN ('проведено', 'отменено', 'ожидается')),
    is_teacher_present BOOLEAN DEFAULT FALSE,
    hours_count NUMERIC(4, 1)
);

-- 6. Таблица оценок
CREATE TABLE grades (
    id SERIAL PRIMARY KEY,
    schedule_id INT REFERENCES schedule(id) ON DELETE CASCADE,
    student_name VARCHAR(100) NOT NULL,
    grade_value INT CHECK (grade_value BETWEEN 2 AND 5),
    grade_date DATE DEFAULT CURRENT_DATE
);

-- ЗАПОЛНЕНИЕ ДАННЫМИ

-- Группы
INSERT INTO groups (name, student_count, course) VALUES
('ГД/1', 17, 1), ('ГД/2', 15, 2), ('ГД/3', 15, 3), ('ГД/4', 14, 4),
('РПО/1', 18, 1), ('РПО/2', 17, 2), ('РПО/3', 15, 3), ('РПО/4', 14, 4);

-- Предметы
INSERT INTO subjects (name) VALUES
('Английский язык'),
('Основы информационных технологий (ОИТ)'),
('Основы программирования'),
('Математика'),
('Физика'),
('Основы языка С++'),
('Введение в специальность'),
('Химия'),
('Биология'),
('Физра');

-- Преподаватели
INSERT INTO teachers (full_name, email, birth_year, experience_years, specialization) VALUES
('Кузнецов Артемий Валерьевич', 'a.kuznetsov@tech-univ.ru', 1985, 12, 'Разработка ПО, Базы данных, C++, Алгоритмы, Искусственный интеллект'),
('Волкова Мирослава Дмитриевна', 'm.volkova@institute.edu', 1990, 8, 'Лингвистика, Иностранные языки'),
('Лебедев Ростислав Игоревич', 'r.lebedev@prof-edu.ru', 1982, 15, 'Программирование, Математика, Дискретная математика'),
('Соколова Евангелина Андреевна', 'e.sokolova@univer.net', 1988, 10, 'Высшая математика, Физика'),
('Морозов Даниил Максимович', 'd.morozov@college.ru', 1979, 20, 'Общая физика, Механика, Термодинамика, Оптика'),
('Степанов Илья Олегович', 'i.stepanov@tech-univ.ru', 1995, 5, 'C++, ООП'),
('Никитина Алиса Павловна', 'a.nikitina@institute.edu', 1992, 7, 'Информационные технологии, ОИТ, Сетевые технологии, Базы данных'),
('Орлов Максим Сергеевич', 'm.orlov@prof-edu.ru', 1987, 11, 'Органическая химия, Лабораторный анализ, Неорганическая химия'),
('Федорова Виктория Андреевна', 'v.fedorova@univer.net', 1993, 6, 'Биология, Экология, Анатомия, Ботаника, Зоология'),
('Белов Артем Дмитриевич', 'a.belov@college.ru', 1980, 18, 'Физическая культура, Спортивные игры');

-- Отпуска (для теста)
INSERT INTO absences (teacher_id, start_date, end_date, type, reason) VALUES
(3, '2024-06-01', '2024-06-14', 'отпуск', 'Ежегодный оплачиваемый отпуск');

-- Расписание
INSERT INTO schedule (teacher_id, subject_id, group_id, auditorium_number, day_of_week, lesson_date, start_time, end_time, status, is_teacher_present, hours_count) VALUES
(6, 6, 5, '101', 1, '2024-05-20', '09:00', '10:30', 'проведено', TRUE, 1.5),
(7, 7, 1, '102', 1, '2024-05-20', '10:40', '12:10', 'ожидается', FALSE, 1.5),
(8, 8, 2, 'Хим-1', 2, '2024-05-21', '13:00', '14:30', 'проведено', TRUE, 1.5),
(9, 9, 6, 'Био-2', 2, '2024-05-21', '14:40', '16:10', 'отменено', FALSE, 0.0),
(10, 10, 3, 'Спортзал', 3, '2024-05-22', '09:00', '10:30', 'ожидается', FALSE, 1.5),
(1, 2, 7, '101', 3, '2024-05-22', '10:40', '12:10', 'ожидается', FALSE, 1.5),
(3, 3, 4, '103', 4, '2024-05-23', '13:00', '14:30', 'проведено', TRUE, 1.5);

-- Оценки (для теста)
INSERT INTO grades (schedule_id, student_name, grade_value) VALUES
(1, 'Абрамов Кирилл', 5),
(1, 'Борисов Егор', 4),
(3, 'Власова Дарья', 2),
(3, 'Гусев Максим', 5);
