import { AxiosError } from 'axios';

export interface ErrorResponse {
  message: string;
}
export const handleError = (reason: AxiosError<ErrorResponse>) => {
  if (reason.response !== null) {
    return reason.response.data.message;
  } else {
    return 'Nieoczekiwany problem.';
  }
};
