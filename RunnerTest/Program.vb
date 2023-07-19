Imports System
Imports KanbanLib
Imports KanbanAPI.Services
Imports KanbanAPI.Models

Module Program
    Sub Main(args As String())
        Dim sqlite As ISqlite = New SqliteService()
        Dim cardCrud As ICardCrud = New SqliteCardCrudService(sqlite)


        Dim fakeCard As Card = New Card(1, "Aprender VB.NET", "Por qué no", Priority.Critical)
        ' cardCrud.Insert(fakeCard)

        Dim elementos As Card() = cardCrud.All()

        For Each elemento In elementos
            Console.WriteLine(elemento.Title & " " & elemento.Description & " " & elemento.Level)
        Next
    End Sub
End Module
