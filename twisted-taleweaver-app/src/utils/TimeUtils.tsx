export const formatTime = (date: Date) => {
  return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
};

export const formatDuration = (start: Date | string, end: Date | string) => {
  const startDate = start instanceof Date ? start : new Date(start);
  const endDate = end instanceof Date ? end : new Date(end);
  
  const diffMs = endDate.getTime() - startDate.getTime(); // difference in milliseconds
  const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));
  const diffHours = Math.floor((diffMs / (1000 * 60 * 60)) % 24);
  const diffMinutes = Math.floor((diffMs / (1000 * 60)) % 60);

  if (diffDays > 0) {
    return `${diffDays}d ${diffHours}h ${diffMinutes}m`;
  }

  if (diffHours > 0) {
    return `${diffHours}h ${diffMinutes}m`;
  }

  return `${diffMinutes}m`;
}