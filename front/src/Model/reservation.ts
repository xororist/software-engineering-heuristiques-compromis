import {User} from './user';
import {ParkingLot} from './parkingMap';


export interface Reservation {
  id: string;
  user: User;
  parkingLot?: ParkingLot | null;
  beginningOfReservation: Date;
  endOfReservation: Date;
  hasBeenConfirmed: boolean;
  hasBeenCancelled: boolean;
}
