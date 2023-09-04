import { toast } from "react-toastify";

export const throwWarning = (obj) => {
    let rep = "";
    if (obj.response.data.errors)
        Object.values(obj.response.data.errors).forEach((element) => {
        rep += element + "\n";
        });
    else rep += obj.response.data.Exception;

    toast.warning(rep);
};

export const dateToString = (date) => {
    return new Date(date).toLocaleDateString("en-GB");
  };
  
  export const dateTimeToString = (date) => {
    return new Date(date).toLocaleString("en-GB");
  };