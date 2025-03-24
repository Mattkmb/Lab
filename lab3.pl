:- encoding(utf8).

% Вычисляет объединение двух множеств (списков без повторяющихся элементов).
% Если элемент из первого списка уже присутствует во втором, он не добавляется повторно.
union_sets([], Set2, Set2).
union_sets([H|T], Set2, Union) :-
    member(H, Set2),
    !,
    union_sets(T, Set2, Union).
union_sets([H|T], Set2, [H|Union]) :-
    \+ member(H, Set2),
    union_sets(T, Set2, Union).

% Интерфейс
main :-
    nl,
    write('Программа для вычисления объединения двух множеств (списки без повторений).'), nl,
    write('Введите первое множество (например, [1,2,3]): '),
    read(Set1),
    write('Введите второе множество (например, [2,4,5]): '),
    read(Set2),
    union_sets(Set1, Set2, Union),
    nl,
    format('Объединение множеств: ~w~n', [Union]),
    nl,
    write('Хотите повторить вычисление? (да/нет): '),
    read(Answer),
    ( Answer == 'да' ->
         nl, main
    ;
         write('До свидания!'), nl, halt
    ).

:- initialization(main).