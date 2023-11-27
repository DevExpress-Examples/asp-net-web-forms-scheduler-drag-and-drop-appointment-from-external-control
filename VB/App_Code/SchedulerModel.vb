Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Web

''' <summary>
''' Summary description for SchedulerModel
''' </summary>

Public Class MySimpleAppointment
	Public Property ID() As Integer
	Public Property ResourceID() As Integer
	Public Property Title() As String
	Public Property StartDate() As DateTime
	Public Property EndDate() As DateTime
	Public Property Label() As String
	Public Property Status() As String
	Public Property MyRecurrenceInfo() As String
	Public Property Type() As Integer
	Public Property Description() As String
End Class
Public Class MySimpleAppointmentAdapter
	Public Function Read() As BindingList(Of MySimpleAppointment)
		Return AppointmentsData
	End Function
	Public Function Create(ByVal postedItem As MySimpleAppointment) As Integer
		Dim newID As Integer = 0
		For Each item As MySimpleAppointment In AppointmentsData
			If newID < item.ID Then
				newID = item.ID
			End If
		Next item
		newID = newID + 1
		AppointmentsData(AppointmentsData.Count - 1).ID = newID
		Return newID
	End Function
	Public Sub Update(ByVal postedItem As MySimpleAppointment)
		Dim editedItem = AppointmentsData.First(Function(i) i.ID = postedItem.ID)
		LoadNewValues(editedItem, postedItem)
	End Sub
	Public Sub Delete(ByVal deletedItem As MySimpleAppointment)
		Dim item = AppointmentsData.First(Function(i) i.ID = deletedItem.ID)
		AppointmentsData.Remove(item)
	End Sub
	Protected Sub LoadNewValues(ByVal newItem As MySimpleAppointment, ByVal postedItem As MySimpleAppointment)
		newItem.Label = postedItem.Label
		newItem.EndDate = postedItem.EndDate
		newItem.MyRecurrenceInfo = postedItem.MyRecurrenceInfo
		newItem.ResourceID = postedItem.ResourceID
		newItem.StartDate = postedItem.StartDate
		newItem.Status = postedItem.Status
		newItem.Title = postedItem.Title
		newItem.Type = postedItem.Type
		newItem.Description = postedItem.Description
	End Sub
	Private ReadOnly Property AppointmentsData() As BindingList(Of MySimpleAppointment)
		Get
			Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
			Dim Session = HttpContext.Current.Session
			If Session(key) Is Nothing Then
				Session(key) = GenerateDS()
			End If
			Return CType(Session(key), BindingList(Of MySimpleAppointment))
		End Get
	End Property
	Public Function GenerateDS() As BindingList(Of MySimpleAppointment)
		Dim model As New BindingList(Of MySimpleAppointment)()
		Return model
	End Function
End Class
Public Class MySimpleResource
	Public Property ResourceID() As Integer
	Public Property ResourceName() As String
End Class
Public Class MySimpleResourceAdapter
	Public Function GetDS() As BindingList(Of MySimpleResource)
		Dim model As New BindingList(Of MySimpleResource)()
		model.Add(New MySimpleResource() With {
			.ResourceID = 1,
			.ResourceName = "Resource1"
		})
		model.Add(New MySimpleResource() With {
			.ResourceID = 2,
			.ResourceName = "Resource2"
		})
		Return model
	End Function
End Class

Public Class MySimpleStandardAppointment
	Public Property Title() As String
	Public Property DurationInHours() As Integer
	Public Property Label() As String
	Public Property Status() As String
	Public Shared Function GenerateDS() As BindingList(Of MySimpleStandardAppointment)
		Dim model As New BindingList(Of MySimpleStandardAppointment)()
		model.Add(New MySimpleStandardAppointment() With {
			.Label = "Red",
			.Status = "Delayed",
			.Title = "App1",
			.DurationInHours = 1
		})
		model.Add(New MySimpleStandardAppointment() With {
			.Label = "Green",
			.Status = "Answered",
			.Title = "App2",
			.DurationInHours = 2
		})
		model.Add(New MySimpleStandardAppointment() With {
			.Label = "Red",
			.Status = "Delayed",
			.Title = "App3",
			.DurationInHours = 1
		})
		model.Add(New MySimpleStandardAppointment() With {
			.Label = "Green",
			.Status = "Answered",
			.Title = "App4",
			.DurationInHours = 7
		})
		model.Add(New MySimpleStandardAppointment() With {
			.Label = "Green",
			.Status = "Delayed",
			.Title = "App5",
			.DurationInHours = 1
		})
		Return model
	End Function
End Class