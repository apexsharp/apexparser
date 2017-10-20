# Sample usage:
# perl -w keywords.pl keywords_reserved.txt > tmp1.cs
# perl -w keywords.pl keywords_nonreserved.txt > tmp2.cs

while (<>)
{
	next if /http/;
	next if /^\s*$/;

	s/\s*$//; # strip newlines
	$reserved = s/\*// ? " // reserved for future use" : ""; # strip stars


	$name = ucfirst($_);
	$value = $_;
	print "        public const string $name = \"$value\";$reserved\n";
}