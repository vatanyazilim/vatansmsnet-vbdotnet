Imports System
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Threading.Tasks

Namespace VatanSms
    Public Class VatanSmsClient
        Private ReadOnly _apiId As String
        Private ReadOnly _apiKey As String
        Private ReadOnly _baseUrl As String
        Private ReadOnly _httpClient As HttpClient

        Public Sub New(apiId As String, apiKey As String, Optional baseUrl As String = "https://api.toplusmspaketleri.com/api/v1")
            If String.IsNullOrEmpty(apiId) OrElse String.IsNullOrEmpty(apiKey) Then
                Throw New ArgumentNullException("API ID and API Key cannot be null or empty.")
            End If
            _apiId = apiId
            _apiKey = apiKey
            _baseUrl = baseUrl.TrimEnd("/"c)
            _httpClient = New HttpClient()
        End Sub

        ''' <summary>
        ''' 1-to-N SMS Gönderimi
        ''' </summary>
        Public Async Function SendSmsAsync(phones As List(Of String), message As String, sender As String, Optional messageType As String = "normal", Optional messageContentType As String = "bilgi", Optional sendTime As DateTime? = Nothing) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .sender = sender,
                .message_type = messageType,
                .message = message,
                .message_content_type = messageContentType,
                .phones = phones,
                .send_time = If(sendTime.HasValue, sendTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Nothing)
            }
            Return Await SendRequestAsync("/1toN", payload)
        End Function

        ''' <summary>
        ''' N-to-N SMS Gönderimi
        ''' </summary>
        Public Async Function SendNtoNSmsAsync(phones As List(Of Dictionary(Of String, String)), sender As String, Optional messageType As String = "turkce", Optional messageContentType As String = "bilgi", Optional sendTime As DateTime? = Nothing) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .sender = sender,
                .message_type = messageType,
                .message_content_type = messageContentType,
                .phones = phones,
                .send_time = If(sendTime.HasValue, sendTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Nothing)
            }
            Return Await SendRequestAsync("/NtoN", payload)
        End Function

        ''' <summary>
        ''' Gönderici Adlarını Sorgulama
        ''' </summary>
        Public Async Function GetSenderNamesAsync() As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey
            }
            Return Await SendRequestAsync("/senders", payload)
        End Function

        ''' <summary>
        ''' Kullanıcı Bilgilerini Sorgulama
        ''' </summary>
        Public Async Function GetUserInformationAsync() As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey
            }
            Return Await SendRequestAsync("/user/information", payload)
        End Function

        ''' <summary>
        ''' Rapor Detayı Sorgulama
        ''' </summary>
        Public Async Function GetReportDetailAsync(reportId As Integer, Optional page As Integer = 1, Optional pageSize As Integer = 20) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .report_id = reportId
            }
            Dim endpoint = $"/report/detail?page={page}&pageSize={pageSize}"
            Return Await SendRequestAsync(endpoint, payload)
        End Function

        ''' <summary>
        ''' Tarih Bazlı Rapor Sorgulama
        ''' </summary>
        Public Async Function GetReportsByDateAsync(startDate As String, endDate As String) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .start_date = startDate,
                .end_date = endDate
            }
            Return Await SendRequestAsync("/report/between", payload)
        End Function

        ''' <summary>
        ''' Sonuç Sorgusu
        ''' </summary>
        Public Async Function GetReportStatusAsync(reportId As Integer) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .report_id = reportId
            }
            Return Await SendRequestAsync("/report/single", payload)
        End Function

        ''' <summary>
        ''' İleri Tarihli SMS İptali
        ''' </summary>
        Public Async Function CancelFutureSmsAsync(id As Integer) As Task(Of String)
            Dim payload = New With {
                .api_id = _apiId,
                .api_key = _apiKey,
                .id = id
            }
            Return Await SendRequestAsync("/cancel/future-sms", payload)
        End Function

        ''' <summary>
        ''' HTTP POST İsteği
        ''' </summary>
        Private Async Function SendRequestAsync(endpoint As String, payload As Object) As Task(Of String)
            Dim jsonPayload = JsonSerializer.Serialize(payload)
            Dim content = New StringContent(jsonPayload, Encoding.UTF8, "application/json")
            Dim response = Await _httpClient.PostAsync($"{_baseUrl}{endpoint}", content)

            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"HTTP Error: {response.StatusCode} - {Await response.Content.ReadAsStringAsync()}")
            End If

            Return Await response.Content.ReadAsStringAsync()
        End Function
    End Class
End Namespace
