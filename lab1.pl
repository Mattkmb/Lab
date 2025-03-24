:- encoding(utf8).

% Предикат для подсчёта количества цифр в числе.
% Если число отрицательное, берём его модуль.
num_digits(Number, Count) :-
    (Number < 0 -> Abs is -Number ; Abs is Number),
    num_digits_helper(Abs, Count).

% Рекурсивная помощь при подсчёте цифр.
num_digits_helper(Number, Count) :-
    ( Number < 10 ->
         Count = 1  
    ;
         Next is Number // 10,           
         num_digits_helper(Next, Count1),
         Count is Count1 + 1
    ).

% Предикат для сравнения двух чисел по количеству цифр.
compare_numbers(Number1, Number2) :-
    num_digits(Number1, Count1),
    num_digits(Number2, Count2),
    ( Count1 > Count2 ->
         format('Первое число (~w) содержит ~w цифр, что больше, чем во втором числе (~w).~n', [Number1, Count1, Count2])
    ; Count2 > Count1 ->
         format('Второе число (~w) содержит ~w цифр, что больше, чем в первом числе (~w).~n', [Number2, Count2, Count1])
    ;
         format('Оба числа (~w и ~w) содержат одинаковое количество цифр: ~w.~n', [Number1, Number2, Count1])
    ).

% Интерфейс
run :-
    nl, write('Добро пожаловать в программу сравнения количества цифр в двух числах!'), nl,
    write('Введите первое число: '),
    read(Number1),
    write('Введите второе число: '),
    read(Number2),
    nl,
    compare_numbers(Number1, Number2),
    nl,
    write('Хотите сравнить другие числа? (да/нет): '),
    read(Answer),
    ( Answer = 'да' ->
         nl, run
    ;
         write('До свидания!'), nl, halt
    ).

:- initialization(run).