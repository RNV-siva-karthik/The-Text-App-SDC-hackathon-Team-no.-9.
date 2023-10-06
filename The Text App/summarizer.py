from distutils.command.install import install


pip install transformers
summarizer = pipeline("summarization", model="facebook/bart-large-cnn")
async def summarize_text(text: str):
    summary = summarizer(text, max_length=130, min_length=30, do_sample=False)
    return summary[0]['summary_text']