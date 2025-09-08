open System

let employees = dict [
    ("employee1", 100)
    ("employee2", 200)
    ("employee3", 300)
    ("employee4", 400)
    ("employee5", 500)
    ("employee6", 600)
    ("employee7", 700)
]

let increaseSalary salary = int (float salary * 1.1)

let updateEmployees = 
    employees
    |> Seq.map (fun kvp -> (kvp.Key, increaseSalary kvp.Value))
    |> type
