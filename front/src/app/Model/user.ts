export enum UserRole {
  USER = 0,
  ADMIN = 1
}

export interface User {
  id: string;
  role: UserRole;
  email: string;
  password: string; // En production, il faudrait hasher les mots de passe
} 