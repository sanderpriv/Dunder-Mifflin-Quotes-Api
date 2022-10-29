<script>
	import Quote from "../components/Quote.svelte";
	import mockDataQuote from "../mockdata/quote.json";

	let showQuote = false;
	function getQuote() {
		showQuote = true;
	}
</script>

<svelte:head>
	<title>Dunder Mifflin Quotes</title>
	<meta name="description" content="Website for generating random quotes from The Office" />
</svelte:head>

<section class="{showQuote ? 'home--show-quote' : 'home'}">
	<button on:click={getQuote}>Get quote</button>

	{#if (showQuote)}
		<Quote quote="{mockDataQuote}" />
	{/if}
</section>

<style>
	.home {
		margin-block: 4rem;
	}

	.home--show-quote {
		margin-block: 4rem;
	}

	section {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	button {
		padding: 0.7rem 3rem;
		font-size: 20px;
		font-family: inherit;

		border: none;
		border-radius: 2rem;

		background-color: var(--color-theme-1);
		box-shadow: 0.125rem 0.125rem 1rem 0 rgba(0, 0, 0, 0.25);
		transition: 0.5s;
	}

	button:hover {
		cursor: pointer;
		box-shadow: 0.125rem 0.125rem 2rem 0 rgba(0, 0, 0, 0.25);
		background-color: var(--color-theme-2);
	}

	button:active {
		 transform: translateY(1rem);
	 }

</style>
