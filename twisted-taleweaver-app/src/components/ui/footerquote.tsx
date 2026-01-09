interface FooterQuoteProps {
  footerQuote: string;
}

export const FooterQuote = ({ footerQuote }: FooterQuoteProps) => {
  return (
    <footer className="text-center py-8">
      <p className="text-muted-foreground font-crimson italic text-lg">
        {footerQuote}
      </p>
      <p className="text-primary font-cinzel text-sm mt-2">
        - The Twisted Taleweaver
      </p>
    </footer>
  );
};