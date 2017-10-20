# Sample usage: perl -w keywords.pl reserved.txt > tmp.cs

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