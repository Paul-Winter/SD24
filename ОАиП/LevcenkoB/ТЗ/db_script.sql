
CREATE TABLE teachers (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100)
);

CREATE TABLE subjects (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE absences (
    id SERIAL PRIMARY KEY,
    teacher_id INT REFERENCES teachers(id) ON DELETE CASCADE,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    type VARCHAR(20) NOT NULL CHECK (type IN ('отпуск', 'больничный', 'выходной')),
    reason TEXT
);

CREATE TABLE schedule (
    id SERIAL PRIMARY KEY,
    teacher_id INT REFERENCES teachers(id) ON DELETE CASCADE,
    subject_id INT REFERENCES subjects(id) ON DELETE CASCADE,
    auditorium_number VARCHAR(20),
    day_of_week INT NOT NULL CHECK (day_of_week BETWEEN 1 AND 7),
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    status VARCHAR(20) DEFAULT 'ожидается' CHECK (status IN ('проведено', 'отменено', 'ожидается')),
    is_teacher_present BOOLEAN DEFAULT FALSE,
    hours_count NUMERIC(4, 1)
);

CREATE TABLE grades (
    id SERIAL PRIMARY KEY,
    schedule_id INT REFERENCES schedule(id) ON DELETE CASCADE,
    student_name VARCHAR(100) NOT NULL,
    grade_value INT CHECK (grade_value BETWEEN 2 AND 5),
    grade_date DATE DEFAULT CURRENT_DATE
);
-- преподаватели и почта
INSERT INTO teachers (full_name, email) VALUES
('Кузнецов Артемий Валерьевич', 'a.kuznetsov@tech-univ.ru'),
('Волкова Мирослава Дмитриевна', 'm.volkova@institute.edu'),
('Лебедев Ростислав Игоревич', 'r.lebedev@prof-edu.ru'),
('Соколова Евангелина Андреевна', 'e.sokolova@univer.net'),
('Морозов Даниил Максимович', 'd.morozov@college.ru');

-- предметы
INSERT INTO subjects (name) VALUES
('Английский язык'),
('Основы информационных технологий (ОИТ)'),
('Основы программирования'),
('Математика'),
('Физика');

-- отпуск
INSERT INTO absences (teacher_id, start_date, end_date, type, reason) VALUES
(3, '2024-06-01', '2024-06-14', 'отпуск', 'Ежегодный оплачиваемый отпуск');

-- расписание от преподавателей
INSERT INTO schedule (teacher_id, subject_id, auditorium_number, day_of_week, start_time, end_time, status, is_teacher_present, hours_count) VALUES
(1, 2, '101', 1, '09:00', '10:30', 'проведено', TRUE, 1.5),  
(2, 1, '102', 1, '10:40', '12:10', 'ожидается', FALSE, 1.5),
(3, 3, '103', 2, '13:00', '14:30', 'проведено', TRUE, 1.5),  
(4, 4, '202', 3, '14:40', '16:10', 'ожидается', FALSE, 1.5), 
(5, 5, '203', 4, '16:20', '17:50', 'ожидается', FALSE, 1.5); 

-- ученики
INSERT INTO grades (schedule_id, student_name, grade_value) VALUES
(1, 'Абрамов Кирилл', 5),
(1, 'Борисов Егор', 4),
(3, 'Власова Дарья', 2),
(3, 'Гусев Максим', 5);
