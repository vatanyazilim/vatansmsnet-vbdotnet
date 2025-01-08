Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports VatanSms
Imports VatanSms.Exceptions

Module VatanSmsClientTests
    Sub Main()
        RunTests().Wait()
    End Sub

    Private Async Function RunTests() As Task
        Await TestSendSmsAsync()
        Await TestSendNtoNSmsAsync()
        Await TestGetSenderNamesAsync()
        Await TestGetUserInformationAsync()
        Await TestGetReportDetailAsync()
        Await TestGetReportsByDateAsync()
        Await TestGetReportStatusAsync()
        Await TestCancelFutureSmsAsync()
    End Function

    Private Async Function TestSendSmsAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim phones = New List(Of String) From {"5xxxxxxxxx"}
            Dim response = Await client.SendSmsAsync(phones, "Test mesajı", "TEST")
            Console.WriteLine("TestSendSmsAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestSendSmsAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestSendNtoNSmsAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim phones = New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"phone", "5xxxxxxxxx"}, {"message", "Mesaj 1"}},
                New Dictionary(Of String, String) From {{"phone", "5xxxxxxxxx"}, {"message", "Mesaj 2"}}
            }
            Dim response = Await client.SendNtoNSmsAsync(phones, "TEST")
            Console.WriteLine("TestSendNtoNSmsAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestSendNtoNSmsAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestGetSenderNamesAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.GetSenderNamesAsync()
            Console.WriteLine("TestGetSenderNamesAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestGetSenderNamesAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestGetUserInformationAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.GetUserInformationAsync()
            Console.WriteLine("TestGetUserInformationAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestGetUserInformationAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestGetReportDetailAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.GetReportDetailAsync(123456, 1, 20)
            Console.WriteLine("TestGetReportDetailAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestGetReportDetailAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestGetReportsByDateAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.GetReportsByDateAsync("2023-01-01", "2023-12-31")
            Console.WriteLine("TestGetReportsByDateAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestGetReportsByDateAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestGetReportStatusAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.GetReportStatusAsync(123456)
            Console.WriteLine("TestGetReportStatusAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestGetReportStatusAsync Error: " & ex.Message)
        End Try
    End Function

    Private Async Function TestCancelFutureSmsAsync() As Task
        Dim client = New VatanSmsClient("test_api_id", "test_api_key")
        Try
            Dim response = Await client.CancelFutureSmsAsync(123)
            Console.WriteLine("TestCancelFutureSmsAsync Response: " & response)
        Catch ex As Exception
            Console.WriteLine("TestCancelFutureSmsAsync Error: " & ex.Message)
        End Try
    End Function
End Module
