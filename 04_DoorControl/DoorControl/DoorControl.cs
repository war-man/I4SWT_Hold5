using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorControl
{
	public class DoorControl
	{
		private readonly IAlarm _alarm;
		private readonly IDoor _door;
		private readonly IEntryNotification _entryNotification;
		private readonly IUserValidation _userValidation;

		private enum DoorControlState
		{
			DoorClosed,
			DoorOpening,
			DoorClosing,
			DoorBreached
		}

		private DoorControlState _state;

		public DoorControl(IAlarm alarm, 
			IDoor door, 
			IEntryNotification entryNotification, 
			IUserValidation userValidation)
		{
			_alarm = alarm;
			_door = door;
			_entryNotification = entryNotification;
			_userValidation = userValidation;

			_state = DoorControlState.DoorClosed;
		}

		void RequestEntry(string id)
		{
			switch (_state)
			{
				case DoorControlState.DoorClosed:
					bool validation = _userValidation.ValidateEntryRequest(id);

					if (validation)
					{
						_door.Open();
						_entryNotification.NotifyEntryGranted();
						_state = DoorControlState.DoorOpening;
					}
					else
					{
						_entryNotification.NotifyEntryDenied();
					}
					break;
			}
		}

		void DoorOpened()
		{
			switch (_state)
			{
				case DoorControlState.DoorClosed:
					_alarm.SignalAlarm();
					_door.Close();
					_state = DoorControlState.DoorBreached;
					break;

				case DoorControlState.DoorOpening:
					_door.Close();
					_state = DoorControlState.DoorClosing;
					break;
			}
		}

		void DoorClosed()
		{
			switch (_state)
			{
				case DoorControlState.DoorClosing:
					_state = DoorControlState.DoorClosed;
					break;
			}
		}
	}
}
