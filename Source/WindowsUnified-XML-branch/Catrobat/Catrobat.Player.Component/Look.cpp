#include "pch.h"
#include "Look.h"
#include "DDSLoader.h"
#include "ProjectDaemon.h"
#include "PlayerException.h"

#include <vector>

using namespace DirectX;

Look::Look(string filename, string name) :
	m_filename(filename), m_name(name)
{
	m_texture = new CatrobatTexture();
}

string Look::GetName()
{
	return m_name;
}

unsigned int Look::GetWidth()
{
	if (m_texture == NULL)
	{
		throw new PlayerException("Look::GetWidth called with no texture defined.");
	}
	return m_texture->width;
}

unsigned int Look::GetHeight()
{
	if (m_texture == NULL)
	{
		throw new PlayerException("Look::GetHeight called with no texture defined.");
	}
	return m_texture->height;
}

string Look::GetFileName()
{
	return m_filename;
}

void Look::LoadTexture(ID3D11Device* d3dDevice)
{
	TextureDaemon::Instance()->LoadTexture(d3dDevice, &m_texture, m_filename);
}

ID3D11ShaderResourceView *Look::GetTexture()
{
	if (m_texture == NULL)
	{
		throw new PlayerException("Look::GetTexture called with no texture defined.");
	}
	return m_texture->texture;
}