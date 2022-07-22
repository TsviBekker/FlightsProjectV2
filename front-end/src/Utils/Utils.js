export const formatDateTime = (date) => {
  if (!date) return "";
  const m = new Date(date);
  const dateString =
    m.getUTCHours() +
    ":" +
    m.getUTCMinutes() +
    ":" +
    m.getUTCSeconds() +
    " " +
    m.getUTCDate() +
    "/" +
    m.getUTCMonth() +
    "/" +
    m.getUTCFullYear();
  return dateString;
};

export const formatPrepTime = (seconds) => {
  return `${seconds > 60 ? seconds / 60 + "min" : ""} ${seconds % 60}sec`;
};
