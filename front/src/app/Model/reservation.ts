export interface Reservation {
  id: string;
  userId: string;
  userRole: string;
  row: string;
  column: number;
  start: string;
  end: string;
  isConfirmed: boolean;
}

export interface CancelReservationRequest {
  reservationId: string;
  userId: string;
} 